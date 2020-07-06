using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SAED.Web.Areas.Identity.IdentityHostingStartup))]
namespace SAED.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}