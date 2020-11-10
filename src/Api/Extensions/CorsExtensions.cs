﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace SAED.Api.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
            allowedOrigins ??= new string[] { };

            services.AddCors(setupAction =>
            {
                setupAction.AddDefaultPolicy(configurePolicy =>
                {
                    configurePolicy
                        .WithOrigins(allowedOrigins)
                        .WithHeaders(HeaderNames.Authorization)
                        .WithMethods(new string[] { HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete });
                });
            });

            return services;
        }
    }
}
