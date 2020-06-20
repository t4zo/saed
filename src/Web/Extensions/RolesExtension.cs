using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAED.Web.Configurations;
using System;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Extensions
{
    public static class RolesExtension
    {
        public static async Task<IApplicationBuilder> CreateRolesAsync(
            this IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IConfiguration configuration
        )
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            if (!roleManager.Roles.AnyAsync().Result)
            {
                var appConfigurations = configuration.GetSection("AppConfiguration").Get<AppConfiguration>();

                foreach (var role in appConfigurations.Roles)
                {
                    var exists = await roleManager.RoleExistsAsync(role);

                    if (!exists)
                    {
                        await roleManager.CreateAsync(new IdentityRole<int> { Name = role, NormalizedName = role.ToUpper() });
                    }

                    await roleManager.AddClaimAsync(roleManager.FindByNameAsync(role).Result, new System.Security.Claims.Claim(CustomClaimTypes.Permission, Permissions.Users.View));
                }
            }

            return app;
        }
    }
}
