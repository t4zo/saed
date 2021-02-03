using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Authorization;
using SAED.Web.Extensions;
using SAED.Web.Options;
using SAED.Web.Services;
using System;

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

            services.AddHttpContextAccessor();

            services.AddScoped<UserService>();

            //services.Configure<AppOptions>(Configuration.GetSection(nameof(AppOptions)));
            services.AddOptions<AppOptions>().Bind(Configuration.GetSection(nameof(AppOptions)));

            services.AddDbContext<ApplicationDbContext>();

            services.AddDefaultIdentity<ApplicationUser>(configureOptions =>
                {
                    configureOptions.Password.RequireDigit = false;
                    configureOptions.Password.RequireLowercase = false;
                    configureOptions.Password.RequireNonAlphanumeric = false;
                    configureOptions.Password.RequireUppercase = false;
                    configureOptions.Password.RequiredLength = 6;
                    configureOptions.Password.RequiredUniqueChars = 1;

                    configureOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
                    configureOptions.Lockout.MaxFailedAccessAttempts = 10;
                    configureOptions.Lockout.AllowedForNewUsers = false;

                    configureOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    configureOptions.User.RequireUniqueEmail = false;
                })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Unspecified;

                options.IdleTimeout = TimeSpan.FromHours(12);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "SAEDCookie";
                options.Cookie.HttpOnly = true;

                options.LoginPath = "/";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = false;

                options.Cookie.SameSite = SameSiteMode.Unspecified;

                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            // services.AddResponseCaching();
            // services.AddResponseCompression();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            string[] supportedCultures = {"pt-BR"};
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            // app.UseRequestLocalization();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            // app.UseResponseCaching();
            // app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    "administrador",
                    "Administrador",
                    "{area=Administrador}/{controller=Home}/{action=Index}/{id?}"
                ).RequireAuthorization();

                endpoints.MapAreaControllerRoute(
                    "aplicador",
                    "Aplicador",
                    "{area=Aplicador}/{controller=Home}/{action=Index}/{id?}"
                ).RequireAuthorization();

                endpoints.MapAreaControllerRoute(
                    "api",
                    "Api",
                    "{area=Api}/{controller=Home}/{action=Index}/{id?}"
                ).RequireAuthorization();

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}