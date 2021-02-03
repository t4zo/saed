using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.Api.Options;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Api.Extensions
{
    public static class UsersExtensions
    {
        public static async Task<IApplicationBuilder> CreateUsersAsync(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var appOptions = serviceProvider.GetRequiredService<IOptionsSnapshot<AppOptions>>().Value;

            if (!await context.Users.AnyAsync())
            {
                foreach (var user in appOptions.Users)
                {
                    var applicationUser = new ApplicationUser {Email = user.Email, UserName = user.UserName};
                    var result = await userManager.CreateAsync(applicationUser, user.Password);

                    if (result.Succeeded)
                    {
                        foreach (var role in user.Roles)
                        {
                            await userManager.AddToRoleAsync(applicationUser, role);
                            await SeedUserClaims(userManager, applicationUser, role);
                            await SeedRoleClaims(roleManager, role);
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }

            return app;
        }

        private static async Task SeedUserClaims(UserManager<ApplicationUser> userManager, ApplicationUser user, string roleName)
        {
            if (roleName.Equals(Roles.Administrador))
            {
                await userManager.AddClaimsAsync(user, new List<Claim>
                {
                    new(CustomClaimTypes.Permission, Permissions.Dashboard.View),
                    new(CustomClaimTypes.Permission, Permissions.Avaliacoes.View),
                    new(CustomClaimTypes.Permission, Permissions.Avaliacoes.Create),
                    new(CustomClaimTypes.Permission, Permissions.Avaliacoes.Update),
                    new(CustomClaimTypes.Permission, Permissions.Avaliacoes.Delete),
                    new(CustomClaimTypes.Permission, Permissions.Escolas.View),
                    new(CustomClaimTypes.Permission, Permissions.Escolas.Create),
                    new(CustomClaimTypes.Permission, Permissions.Escolas.Update),
                    new(CustomClaimTypes.Permission, Permissions.Escolas.Delete),
                    new(CustomClaimTypes.Permission, Permissions.Disciplinas.View),
                    new(CustomClaimTypes.Permission, Permissions.Disciplinas.Create),
                    new(CustomClaimTypes.Permission, Permissions.Disciplinas.Update),
                    new(CustomClaimTypes.Permission, Permissions.Disciplinas.Delete)
                });
            }

            if (roleName.Equals(Roles.Aplicador))
            {
                await userManager.AddClaimsAsync(user, new List<Claim>
                {
                    new(CustomClaimTypes.Permission, Permissions.Avaliacoes.View),
                    new(CustomClaimTypes.Permission, Permissions.Escolas.View),
                    new(CustomClaimTypes.Permission, Permissions.Escolas.View),
                    new(CustomClaimTypes.Permission, Permissions.Disciplinas.View),
                    new(CustomClaimTypes.Permission, Permissions.Selecao.View)
                });
            }
        }

        private static async Task SeedRoleClaims(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (roleName.Equals(Roles.Administrador))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Dashboard.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Create));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Update));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Delete));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Create));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Update));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Delete));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Create));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Update));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Delete));
            }

            if (roleName.Equals(Roles.Aplicador))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.View));
            }
        }
    }
}