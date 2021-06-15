﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using MagasBook.Application.Common.Interfaces;
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}