using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SAED.ApplicationCore.Constants;

namespace SAED.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (Environment.GetEnvironmentVariable(ProvidersConstants.Provider) ==
                        ProvidersConstants.DigitalOcean)
                    {
                        webBuilder.UseStartup<Startup>().UseUrls("http://localhost:5000");
                    }
                    else
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                });
        }
    }
}