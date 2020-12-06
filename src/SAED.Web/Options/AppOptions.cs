using System.Collections.Generic;

namespace SAED.Web.Options
{
    public class AppOptions
    {
        public ICollection<string> Roles { get; set; }
        public ICollection<UserOptions> Users { get; set; }
    }
}