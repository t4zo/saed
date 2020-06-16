using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Configurations;
using System;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Extensions
{
    public static class UsersExtension
    {
        public static async Task<IApplicationBuilder> CreateUsers(
            this IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IConfiguration configuration
            )
        {
            var context = serviceProvider.GetRequiredService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var hasUser = await context.Users.AnyAsync();

            if (!hasUser)
            {
                var appConfiguration = configuration.GetSection("AppConfiguration").Get<AppConfiguration>();

                foreach (var _user in appConfiguration.Users)
                {
                    var user = new ApplicationUser { UserName = _user.UserName, Email = _user.Email };
                    var result = await userManager.CreateAsync(user, _user.Password);

                    if (result.Succeeded)
                    {
                        await userManager.AddClaimAsync(user, new System.Security.Claims.Claim(CustomClaimTypes.Permission, Permissions.Users.View));
                    }

                    foreach (var role in _user.Roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    };
                };
            }

            return app;
        }
    }
}
