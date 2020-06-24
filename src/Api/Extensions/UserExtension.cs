using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SAED.Api.Configurations;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using System;
using System.Threading.Tasks;

namespace SAED.Api.Extensions
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
                    var user = new ApplicationUser { Email = _user.Email, UserName = _user.UserName };
                    var result = await userManager.CreateAsync(user, _user.Password);

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
