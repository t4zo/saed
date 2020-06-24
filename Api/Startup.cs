using AutoMapper;
using Infrastructure.i18n;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAED.Api.Extensions;
using SAED.Api.Interfaces;
using SAED.Api.Services;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Services;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using System;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddTransient<IUnityOfWork, UnityOfWorkService>();
            services.AddTransient<IUserService, UserService>();

            services.AddCustomCors(DefaultCorsPolicyName);

            services.AddDbContext<ApplicationDbContext>();

            services.AddControllers();
            services.AddJwtSecurity(Configuration);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Roles.AllName, policy => policy.RequireRole(Roles.All));
                options.AddPolicy(Roles.Administrador, policy => policy.RequireRole(Roles.Administrador));
                options.AddPolicy(Roles.Aplicador, policy => policy.RequireRole(Roles.Aplicador));
            });

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager();

            services.AddAutoMapper(typeof(Startup));

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.IsInDocker(serviceProvider, Configuration);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.CreateRoles(serviceProvider, Configuration).Wait();
            app.CreateUsers(serviceProvider, Configuration).Wait();

            app.UseCors(DefaultCorsPolicyName);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            // app.UseSwagger();
            // app.ConfigureSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapControllers()
                .RequireAuthorization();
            });
        }
    }
}
