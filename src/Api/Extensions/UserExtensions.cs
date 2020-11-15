using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.Api.Configurations;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Api.Extensions
{
    public static class UsersExtension
    {
        public static async Task<IApplicationBuilder> CreateUsers(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var appConfiguration = serviceProvider.GetRequiredService<IOptionsSnapshot<AppConfiguration>>().Value;

            if (!await context.Users.AnyAsync())
            {
                foreach (var user in appConfiguration.Users)
                {
                    var applicationUser = new ApplicationUser { Email = user.Email, UserName = user.UserName };
                    var result = await userManager.CreateAsync(applicationUser, user.Password);

                    if (result.Succeeded)
                    {
                        foreach (var role in user.Roles)
                        {
                            await userManager.AddToRoleAsync(applicationUser, role);

                            await SeedUserClaims(userManager, applicationUser, role);

                            await SeedRoleClaims(roleManager, role);
                        };
                    }

                    await AddUsuarioTurmaAvaliacao(userManager, applicationUser, context);

                    await context.SaveChangesAsync();
                };
            }

            return app;
        }

        private static async Task AddUsuarioTurmaAvaliacao(UserManager<ApplicationUser> userManager, ApplicationUser user, ApplicationDbContext context)
        {
            if (await userManager.IsInRoleAsync(user, Roles.Aplicador))
            {
                var avaliacoes = await context.Avaliacoes.Where(a => a.Status == StatusAvaliacao.EmAndamento).ToListAsync();
                foreach (var avaliacao in avaliacoes)
                {
                    context.Add(new UsuarioTurmaAvaliacao { ApplicationUserId = user.Id, AvaliacaoId = avaliacao.Id, TurmaId = 1 });
                }
            }
        }

        private static async Task SeedUserClaims(UserManager<ApplicationUser> userManager, ApplicationUser user, string role)
        {
            if (role.Equals(Roles.Administrador))
            {
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Dashboard.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Create));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Update));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Delete));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Create));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Update));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Delete));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Create));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Update));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Delete));
            }

            if (role.Equals(Roles.Aplicador))
            {
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
            }
        }

        private static async Task SeedRoleClaims(RoleManager<ApplicationRole> roleManager, string role)
        {
            if (role.Equals(Roles.Administrador))
            {
                var _role = await roleManager.FindByNameAsync(role);
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Dashboard.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Create));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Update));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Delete));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Create));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Update));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Delete));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Create));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Update));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Delete));
            }

            if (role.Equals(Roles.Aplicador))
            {
                var _role = await roleManager.FindByNameAsync(role);
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
            }
        }
    }
}
