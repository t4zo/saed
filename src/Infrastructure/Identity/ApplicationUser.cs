using System;
using Microsoft.AspNetCore.Identity;

namespace SAED.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime LastLogin { get; set; }
    }
}