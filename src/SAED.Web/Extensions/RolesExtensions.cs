﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.Core.Extensions;
using SAED.Persistence.Identity;
using SAED.Web.Options;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Extensions
{
    public static class RolesExtensions
    {
        public static async Task<IServiceProvider> CreateRolesAsync(this IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var appOptions = serviceProvider.GetRequiredService<IOptionsSnapshot<AppOptions>>().Value;

            if (!await roleManager.Roles.AnyAsync())
            {
                foreach (var role in appOptions.Roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var newRole = new ApplicationRole { Name = role, NormalizedName = role.ToUpper() };
                        await roleManager.CreateAsync(newRole);
                    }

                    await SeedRoleClaims(roleManager, role);
                }
            }

            return serviceProvider;
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
            else if (role.Name.Equals(Roles.Administrador))
            {
                foreach (var permission in permissions)
                {
                    if (!permission.Contains("Grupos") && !permission.Contains("Usuarios"))
                    {
                        await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                    }
                }
            }
            else if (role.Name.Equals(Roles.Aplicador))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.DashboardAplicador.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Selecao.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Aplicacao.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Aplicacao.Create));
            }
            else if (role.Name.Equals(Roles.Aluno))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.DashboardAplicador.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Aplicacao.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Aplicacao.Create));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Selecao.View));
            }
        }
    }
}