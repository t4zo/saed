using System.Collections.Generic;

namespace SAED.Web.Options
{
    public class UserOptions
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}