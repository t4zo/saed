using Microsoft.Extensions.DependencyInjection;

namespace SAED.Api.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection service, string _defaultCorsPolicyName)
        {
            service.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, configurePolicy =>
                {
                    configurePolicy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return service;
        }
    }
}
