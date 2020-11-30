using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Configurations;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Extensions
{
    public static class UsersExtensions
    {
        public static async Task<IApplicationBuilder> CreateUsersAsync(this IApplicationBuilder app,
            IServiceProvider serviceProvider)
        {
            ApplicationDbContext context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<ApplicationRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            AppConfiguration appConfiguration =
                serviceProvider.GetRequiredService<IOptionsSnapshot<AppConfiguration>>().Value;

            if (!await context.Users.AnyAsync())
            {
                foreach (UserConfiguration user in appConfiguration.Users)
                {
                    ApplicationUser applicationUser =
                        new ApplicationUser {Email = user.Email, UserName = user.UserName};
                    IdentityResult result = await userManager.CreateAsync(applicationUser, user.Password);

                    if (result.Succeeded)
                    {
                        foreach (string role in user.Roles)
                        {
                            await userManager.AddToRoleAsync(applicationUser, role);

                            await SeedUserClaims(userManager, applicationUser, role);

                            await SeedRoleClaims(roleManager, role);
                        }

                        ;
                    }

                    await AddUsuarioTurmaAvaliacao(userManager, applicationUser, context);

                    await context.SaveChangesAsync();
                }

                ;
            }

            return app;
        }

        private static async Task AddUsuarioTurmaAvaliacao(UserManager<ApplicationUser> userManager,
            ApplicationUser user, ApplicationDbContext context)
        {
            if (await userManager.IsInRoleAsync(user, Roles.Aplicador))
            {
                List<Avaliacao> avaliacoes =
                    await context.Avaliacoes.Where(a => a.Status == StatusAvaliacao.EmAndamento).ToListAsync();
                foreach (Avaliacao avaliacao in avaliacoes)
                {
                    context.Add(new UsuarioTurmaAvaliacao
                    {
                        ApplicationUserId = user.Id, AvaliacaoId = avaliacao.Id, TurmaId = 1
                    });
                }
            }
        }

        private static async Task SeedUserClaims(UserManager<ApplicationUser> userManager, ApplicationUser user,
            string role)
        {
            if (role.Equals(Roles.Administrador))
            {
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Dashboard.View));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Create));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Update));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Delete));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Create));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Update));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Delete));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Create));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Update));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Delete));
            }

            if (role.Equals(Roles.Aplicador))
            {
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
                await userManager.AddClaimAsync(user,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Selecao.View));
            }
        }

        private static async Task SeedRoleClaims(RoleManager<ApplicationRole> roleManager, string role)
        {
            if (role.Equals(Roles.Administrador))
            {
                ApplicationRole _role = await roleManager.FindByNameAsync(role);
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Dashboard.View));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Create));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Update));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.Delete));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Create));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Update));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.Delete));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Create));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Update));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.Delete));
            }

            if (role.Equals(Roles.Aplicador))
            {
                ApplicationRole _role = await roleManager.FindByNameAsync(role);
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(_role,
                    new Claim(CustomClaimTypes.Permissions, Permissions.Disciplinas.View));
            }
        }
    }
}