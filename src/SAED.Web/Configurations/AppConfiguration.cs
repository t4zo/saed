using System.Collections.Generic;

namespace SAED.Web.Configurations
{
    public class AppConfiguration
    {
        public ICollection<string> Roles { get; set; }
        public ICollection<UserConfiguration> Users { get; set; }
    }
}