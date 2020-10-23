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
            var context = serviceProvider.GetRequiredService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var appConfiguration = serviceProvider.GetRequiredService<IOptionsSnapshot<AppConfiguration>>();

            if (!await context.Users.AnyAsync())
            {
                foreach (var _user in appConfiguration.Value.Users)
                {
                    var user = new ApplicationUser { Email = _user.Email, UserName = _user.UserName };
                    var result = await userManager.CreateAsync(user, _user.Password);

                    if (result.Succeeded)
                    {
                        foreach (var role in _user.Roles)
                        {
                            await userManager.AddToRoleAsync(user, role);

                            await SeedUserClaims(userManager, user, role);

                            await SeedRoleClaims(roleManager, role);
                        };
                    }

                    await AddUsuarioTurmaAvaliacao(userManager, user, context);

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
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Create));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Update));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Delete));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Create));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Update));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Delete));
            }

            if (role.Equals(Roles.Aplicador))
            {
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.View));
                await userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.View));
            }
        }

        private static async Task SeedRoleClaims(RoleManager<IdentityRole<int>> roleManager, string role)
        {
            if (role.Equals(Roles.Administrador))
            {
                IdentityRole<int> _role = await roleManager.FindByNameAsync(role);
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Dashboard.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Create));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Update));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.Delete));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Create));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Update));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.Delete));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Create));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Update));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.Delete));
            }

            if (role.Equals(Roles.Aplicador))
            {
                IdentityRole<int> _role = await roleManager.FindByNameAsync(role);
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Avaliacoes.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Escolas.View));
                await roleManager.AddClaimAsync(_role, new Claim(CustomClaimTypes.Permission, Permissions.Disciplinas.View));
            }
        }
    }
}
