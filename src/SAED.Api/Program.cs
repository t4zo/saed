using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SAED.Core.Constants;
using System;
using System.Net;

namespace SAED.Api
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
                    webBuilder.UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5000);
                    });

                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}