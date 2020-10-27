using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Authorization;
using SAED.Web.Configurations;
using SAED.Web.Extensions;
using SAED.Web.Services;
using System;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<UserService>();

            services.Configure<AppConfiguration>(Configuration.GetSection(nameof(AppConfiguration)));

            services.AddDbContext<ApplicationDbContext>();

            services.AddCustomCors(DefaultCorsPolicyName);

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = false;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;

                options.IdleTimeout = TimeSpan.FromHours(12);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "SAEDCookie";
                options.Cookie.HttpOnly = true;

                options.LoginPath = "/";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;

                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddResponseCompression();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.CreateRolesAsync(serviceProvider).GetAwaiter().GetResult();
            app.CreateUsersAsync(serviceProvider).GetAwaiter().GetResult();
            app.SeedDatabase(serviceProvider);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            //app.UseRequestLocalization();
            app.UseCors(DefaultCorsPolicyName);

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            // app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "administrador",
                    areaName: "Administrador",
                    pattern: "{area=Administrador}/{controller=Home}/{action=Index}/{id?}"
                ).RequireAuthorization();

                endpoints.MapAreaControllerRoute(
                    name: "api",
                    areaName: "Api",
                    pattern: "{area=Api}/{controller=Home}/{action=Index}/{id?}"
                ).RequireAuthorization();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
