using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.Api.Configurations;
using SAED.Infrastructure.Identity;
using System;
using System.Threading.Tasks;

namespace SAED.Api.Extensions
{
    public static class RolesExtensions
    {
        public static async Task<IApplicationBuilder> CreateRoles(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var appConfiguration = serviceProvider.GetRequiredService<IOptionsSnapshot<AppConfiguration>>();

            if (!roleManager.Roles.AnyAsync().Result)
            {
                foreach (var role in appConfiguration.Value.Roles)
                {
                    var exists = await roleManager.RoleExistsAsync(role);

                    if (!exists)
                    {
                        await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
                    }
                }
            }

            return app;
        }
    }
}
