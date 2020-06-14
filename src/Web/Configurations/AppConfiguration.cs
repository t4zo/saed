using System.Collections.Generic;

namespace SAED.Web.Configurations
{
    public class AppConfiguration
    {
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<UserConfiguration> Users { get; set; }
    }
}
