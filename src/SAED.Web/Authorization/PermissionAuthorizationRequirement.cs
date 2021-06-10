using Microsoft.AspNetCore.Authorization;
using System;

namespace SAED.Web.Authorization
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionAuthorizationRequirement(string permission)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }
    }
}