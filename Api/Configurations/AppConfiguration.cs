using System.Collections.Generic;

namespace SAED.Api.Configurations
{
    public class AppConfiguration
    {
        public TokenConfiguration Token { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<UserConfiguration> Users { get; set; }
    }
}
