using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace SAED.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Heroku")
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                    else
                    {
                        var PORT = Environment.GetEnvironmentVariable("PORT");

                        webBuilder.UseStartup<Startup>()
                        .UseUrls($"http://*:{PORT}");
                    }
                });
    }
}
