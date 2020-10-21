using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SAED.ApplicationCore.Entities;
using System.Net;
using System.Text.Json;

namespace SAED.Api.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
                        logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error.",
                            //Detailed = exceptionHandlerFeature.Error
                        }.ToJson());

                        //var json = new
                        //{
                        //    context.Response.StatusCode,
                        //    Message = "An error occurred whilst processing your request",
                        //    Detailed = exceptionHandlerFeature.Error
                        //};

                        //await context.Response.WriteAsync(JsonSerializer.Serialize(json));
                    }
                });
            });
        }
    }
}
