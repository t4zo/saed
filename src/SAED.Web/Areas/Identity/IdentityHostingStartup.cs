using Microsoft.AspNetCore.Hosting;
using SAED.Web.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace SAED.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}