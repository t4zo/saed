using Microsoft.AspNetCore.Authorization;
using System;

namespace SAED.Api.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }
    }
}