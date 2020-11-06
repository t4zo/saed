using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.ApplicationCore.Extensions;
using SAED.Infrastructure.Identity;
using SAED.Web.Configurations;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Extensions
{
    public static class RolesExtensions
    {
        public static async Task<IApplicationBuilder> CreateRolesAsync(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var appConfiguration = serviceProvider.GetRequiredService<IOptionsSnapshot<AppConfiguration>>().Value;

            if (!await roleManager.Roles.AnyAsync())
            {
                foreach (var role in appConfiguration.Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
                    }

                    await SeedRoleClaims(roleManager, role);
                }
            }

            return app;
        }

        private static async Task SeedRoleClaims(RoleManager<ApplicationRole> roleManager, string role)
        {
            var permissions = typeof(Permissions).GetAllPublicConstantValues<string>();

            if (role.Equals(Roles.Superuser))
            {
                foreach (var permission in permissions)
                {
                    await roleManager.AddClaimAsync(roleManager.FindByNameAsync(role).GetAwaiter().GetResult(), new Claim(CustomClaimTypes.Permission, permission));
                }
            }

            if (role.Equals(Roles.Administrador))
            {
                foreach (var permission in permissions)
                {
                    if (!permission.Contains("Grupos") && !permission.Contains("Usuarios"))
                    {
                        await roleManager.AddClaimAsync(roleManager.FindByNameAsync(role).GetAwaiter().GetResult(), new Claim(CustomClaimTypes.Permission, permission));
                    }
                }
            }

            if (role.Equals(Roles.Aplicador))
            {
                foreach (var permission in permissions)
                {
                    if ((permission.Contains("View") || permission.Contains("Selecao")) && !(permission.Contains("Grupos") || permission.Contains("Usuarios")))
                    {
                        await roleManager.AddClaimAsync(roleManager.FindByNameAsync(role).GetAwaiter().GetResult(), new Claim(CustomClaimTypes.Permission, permission));
                    }
                }
            }
        }
    }
}
