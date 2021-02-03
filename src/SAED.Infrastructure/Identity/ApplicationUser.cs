using Microsoft.AspNetCore.Identity;
using System;

namespace SAED.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime LastLogin { get; set; }
    }
}