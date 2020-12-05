using System.Collections.Generic;

namespace SAED.Api.Configurations
{
    public class AppConfiguration
    {
        public TokenConfiguration Token { get; set; }
        public ICollection<string> Roles { get; set; }
        public ICollection<UserConfiguration> Users { get; set; }
    }
}