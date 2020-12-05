using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.Core.Extensions;
using SAED.Infrastructure.Identity;
using SAED.Web.Configurations;
using static SAED.Core.Constants.AuthorizationConstants;

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
                        var newRole = new ApplicationRole {Name = role, NormalizedName = role.ToUpper()};
                        await roleManager.CreateAsync(newRole);
                    }

                    await SeedRoleClaims(roleManager, role);
                }
            }

            return app;
        }

        private static async Task SeedRoleClaims(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var permissions = typeof(Permissions).GetAllPublicConstantValues<string>();
            var role = await roleManager.FindByNameAsync(roleName);

            if (role.Name.Equals(Roles.Superuser))
            {
                foreach (var permission in permissions)
                {
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                }
            }

            if (role.Name.Equals(Roles.Administrador))
            {
                foreach (var permission in permissions)
                {
                    if (!permission.Contains("Grupos") && !permission.Contains("Usuarios"))
                    {
                        await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                    }
                }
            }

            if (role.Name.Equals(Roles.Aplicador))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.DashboardAplicador.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Selecao.View));
            }
        }
    }
}