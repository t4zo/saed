﻿using System;
using Microsoft.AspNetCore.Authorization;

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