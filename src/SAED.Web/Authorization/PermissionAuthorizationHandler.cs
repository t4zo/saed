﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SAED.Persistence.Identity;
using System.Linq;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);

            if (user is null)
            {
                return;
            }

            if (await _userManager.IsInRoleAsync(user, Roles.Superuser))
            {
                context.Succeed(requirement);
                return;
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userPermissions = userClaims.Where(x =>
                x.Type == CustomClaimTypes.Permission &&
                x.Value == requirement.Permission &&
                x.Issuer == "LOCAL AUTHORITY"
            ).Select(x => x.Value);

            if (userPermissions.Any())
            {
                context.Succeed(requirement);
                return;
            }

            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).ToList();

            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                var rolePermissions = roleClaims.Where(x =>
                    x.Type == CustomClaimTypes.Permission &&
                    x.Value == requirement.Permission &&
                    x.Issuer == "LOCAL AUTHORITY"
                ).Select(x => x.Value);

                if (rolePermissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}