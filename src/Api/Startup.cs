using AutoMapper;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SAED.Api.Authorization;
using SAED.Api.Configurations;
using SAED.Api.Extensions;
using SAED.Api.Interfaces;
using SAED.Api.Services;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.i18n;
using SAED.Infrastructure.Identity;
using System;
using System.Reflection;
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
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.Configure<AppConfiguration>(Configuration.GetSection(nameof(AppConfiguration)));

            services.AddDbContext<ApplicationDbContext>();

            services.AddCustomCors(DefaultCorsPolicyName);

            services.AddControllers()
                .AddFluentValidation(configureExpression => configureExpression.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddJwtSecurity(Configuration);

            services.AddProblemDetails();

            services.AddAuthorization();

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwagger();

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

            app.UseProblemDetails();

            app.CreateRoles(serviceProvider).GetAwaiter().GetResult();
            app.CreateUsers(serviceProvider).GetAwaiter().GetResult();
            app.SeedDatabase(serviceProvider);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseConfiguredSwagger();

            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
