using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SAED.Api.Configurations;

namespace SAED.Api.Extensions
{
    public static class JwtSecurityExtensions
    {
        public static IServiceCollection AddJwtSecurity(
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            // configure strongly typed settings objects
            var appConfigurationSection = configuration.GetSection("AppConfiguration");
            services.Configure<AppConfiguration>("AppConfiguration", appConfigurationSection);

            // configure jwt authentication
            var appConfiguration = appConfigurationSection.Get<AppConfiguration>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = appConfiguration.Token.Key,

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = appConfiguration.Token.Issuer,

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = false,
                    ValidAudience = appConfiguration.Token.Audience,

                    // Validate the token expiry
                    ValidateLifetime = true,
                };
            }).AddCookie(IdentityConstants.ApplicationScheme);

            return services;
        }
    }
}
