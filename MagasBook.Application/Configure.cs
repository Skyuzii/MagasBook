using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MagasBook.Application.Interfaces;
using MagasBook.Application.Services;
using MediatR;

namespace MagasBook.Application
{
    public static class Configure
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}