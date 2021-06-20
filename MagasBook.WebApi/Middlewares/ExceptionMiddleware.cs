using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using MagasBook.Application.Dto;
using MagasBook.Application.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace MagasBook.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionsInvokers;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;

            _exceptionsInvokers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
            {
                {typeof(ValidationException), HandleValidationException},
                {typeof(NotFoundException), HandleNotFoundException},
                {typeof(RestException), HandleRestException},
            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                var exType = exception.GetType();
                if (_exceptionsInvokers.ContainsKey(exType))
                {
                    await _exceptionsInvokers[exType].Invoke(context, exception);
                }
                else
                {
                    await HandleUnrecognizedException(context, exception);
                }
            }
        }

        private async Task HandleUnrecognizedException(HttpContext context, Exception exception)
        {
            var error = BuildDefaultErrorResult(exception);

            var result = JsonConvert.SerializeObject(error);
			
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
			
            await context.Response.WriteAsync(result);
        }

        private async Task HandleValidationException(HttpContext context, Exception exception)
        {
            var error = BuildDefaultErrorResult(exception);
			
            var result = JsonConvert.SerializeObject(error);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
			
            await context.Response.WriteAsync(result);
        }
        
        private Task HandleNotFoundException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            return Task.CompletedTask;
        }

        private async Task HandleRestException(HttpContext context, Exception exception)
        {
            var restException = (RestException)exception;
            var error = BuildDefaultErrorResult(restException);
			
            var result = JsonConvert.SerializeObject(error);

            context.Response.StatusCode = restException.StatusCode;
            context.Response.ContentType = "application/json";
			
            await context.Response.WriteAsync(result);
        }

        private BadResponseDto BuildDefaultErrorResult(Exception exception)
        {
            var badResponse = new BadResponseDto
            {
                Errors = new List<string> {exception.Message}
            };

            if (_env.IsDevelopment())
            {
                badResponse.Errors.Add(exception.StackTrace);
            }
            
            return badResponse;
        }
    }

}