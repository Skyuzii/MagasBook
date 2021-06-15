using System;
using MagasBook.Application.Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagasBook.WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected OperationResultDto Success(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                HttpContext.Response.StatusCode = statusCode.Value;
            }
            
            return new OperationResultDto
            {
                Success = true
            };
        }

        protected OperationResultDto<T> Success<T>(T data, int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                HttpContext.Response.StatusCode = statusCode.Value;
            }
            
            return new OperationResultDto<T>
            {
                Success = true,
                Data = data
            };
        }
    }
}