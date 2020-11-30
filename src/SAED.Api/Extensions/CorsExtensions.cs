using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using SAED.Core.Constants;

namespace SAED.Api.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            string[] allowedOrigins = configuration.GetSection(AuthorizationConstants.AllowedOrigins).Get<string[]>();
            allowedOrigins ??= Array.Empty<string>();

            services.AddCors(setupAction =>
            {
                setupAction.AddDefaultPolicy(configurePolicy =>
                {
                    configurePolicy
                        .WithOrigins(allowedOrigins)
                        .WithHeaders(HeaderNames.Authorization)
                        .WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Delete);
                });
            });

            return services;
        }
    }
}