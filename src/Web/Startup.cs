using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Authorization;
using SAED.Web.Extensions;

namespace SAED.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private const string _defaultCorsPolicyName = "localhost";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddDbContext<ApplicationDbContext>();

            services.AddCustomCors(_defaultCorsPolicyName);

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddResponseCompression();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (Configuration["DOCKER"] == "True")
            {
                var context = serviceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.CreateRoles(serviceProvider, Configuration).Wait();
            app.CreateUsers(serviceProvider, Configuration).Wait();
            app.SeedDatabase(serviceProvider).Wait();

            app.UseCors(_defaultCorsPolicyName);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "administrador",
                    areaName: "Administrador",
                    pattern: "{area=Administrador}/{controller=Home}/{action=Index}/{id?}"
                ).RequireAuthorization();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
