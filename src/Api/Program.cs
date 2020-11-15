using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SAED.ApplicationCore.Constants;
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
                    if (Environment.GetEnvironmentVariable(ProvidersConstants.Provider) == ProvidersConstants.DigitalOcean)
                    {
                        webBuilder.UseStartup<Startup>().UseUrls("http://localhost:5100");
                    }
                    else
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                });
    }
}
