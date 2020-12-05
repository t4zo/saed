using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAED.Infrastructure.Data;

namespace SAED.Api.Extensions
{
    public static class DockerExtensions
    {
        public static IApplicationBuilder IsInDocker(this IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (configuration["DOCKER"] == "True")
            {
                var context = serviceProvider.GetService<ApplicationDbContext>();
                context?.Database.Migrate();
            }

            return applicationBuilder;
        }
    }
}