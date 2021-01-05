using Microsoft.AspNetCore.Authorization;
using System;

namespace SAED.Web.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }

        public string Permission { get; }
    }
}