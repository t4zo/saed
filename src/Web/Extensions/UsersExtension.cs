using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Configurations;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Extensions
{
    public static class UsersExtension
    {
        public static async Task<IApplicationBuilder> CreateUsersAsync(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var appConfiguration = serviceProvider.GetRequiredService<IOptionsSnapshot<AppConfiguration>>();

            if (!await context.Users.AnyAsync())
            {
                foreach (var user in appConfiguration.Value.Users)
                {
                    var applicationUser = new ApplicationUser { UserName = user.UserName, Email = user.Email };
                    var result = await userManager.CreateAsync(applicationUser, user.Password);

                    if (result.Succeeded)
                    {
                        foreach (var role in user.Roles)
                        {
                            await userManager.AddToRoleAsync(applicationUser, role);

                            //await SeedUserClaims(userManager, user, role);
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
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Dashboard.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Create));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Update));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Delete));
            }

            if (role.Equals(Roles.Aplicador))
            {
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.View));
            }
        }
    }
}
