using Microsoft.AspNetCore.Identity;
using System;

namespace SAED.Persistence.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime LastLogin { get; set; }
    }
}