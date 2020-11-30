using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.Api.Configurations;
using SAED.Core.Extensions;
using SAED.Infrastructure.Identity;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Api.Extensions
{
    public static class RolesExtensions
    {
        public static async Task<IApplicationBuilder> CreateRoles(this IApplicationBuilder app,
            IServiceProvider serviceProvider)
        {
            RoleManager<ApplicationRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            AppConfiguration appConfiguration =
                serviceProvider.GetRequiredService<IOptionsSnapshot<AppConfiguration>>().Value;

            if (!await roleManager.Roles.AnyAsync())
            {
                foreach (string role in appConfiguration.Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(
                            new ApplicationRole {Name = role, NormalizedName = role.ToUpper()});
                    }

                    await SeedRoleClaims(roleManager, role);
                }
            }

            return app;
        }

        private static async Task SeedRoleClaims(RoleManager<ApplicationRole> roleManager, string role)
        {
            List<string> permissions = typeof(Permissions).GetAllPublicConstantValues<string>();

            if (role.Equals(Roles.Superuser))
            {
                foreach (string permission in permissions)
                {
                    await roleManager.AddClaimAsync(roleManager.FindByNameAsync(role).GetAwaiter().GetResult(),
                        new Claim(CustomClaimTypes.Permissions, permission));
                }
            }

            if (role.Equals(Roles.Administrador))
            {
                foreach (string permission in permissions)
                {
                    if (!permission.Contains("Grupos") && !permission.Contains("Usuarios"))
                    {
                        await roleManager.AddClaimAsync(roleManager.FindByNameAsync(role).GetAwaiter().GetResult(),
                            new Claim(CustomClaimTypes.Permissions, permission));
                    }
                }
            }

            if (role.Equals(Roles.Aplicador))
            {
                foreach (string permission in permissions)
                {
                    if (permission.Contains("View") &&
                        !(permission.Contains("Grupos") || permission.Contains("Usuarios")))
                    {
                        await roleManager.AddClaimAsync(roleManager.FindByNameAsync(role).GetAwaiter().GetResult(),
                            new Claim(CustomClaimTypes.Permissions, permission));
                    }
                }
            }
        }
    }
}