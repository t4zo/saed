using System.Collections.Generic;

namespace SAED.Web.Configurations
{
    public class UserConfiguration
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}