﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SAED.Infrastructure.Identity;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Api.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Modificar completamente para validar pelo Token
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);

            // Se não estiver logado retorna não autorizado
            if (user is null)
            {
                return;
            }

            // Autoriza se for Superusuário
            if (await _userManager.IsInRoleAsync(user, Roles.Superuser))
            {
                context.Succeed(requirement);
                return;
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userPermissions = userClaims.Where(x =>
                x.Type == CustomClaimTypes.Permissions &&
                x.Value == requirement.Permission &&
                x.Issuer == "LOCAL AUTHORITY"
            ).Select(x => x.Value);

            // Autoriza se o usuário possuir a permissão requerida
            if (userPermissions.Any())
            {
                context.Succeed(requirement);
                return;
            }

            var userRoleNames = await _userManager.GetRolesAsync(user);
            var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

            foreach (var role in userRoles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                var rolePermissions = roleClaims.Where(x =>
                    x.Type == CustomClaimTypes.Permissions &&
                    x.Value == requirement.Permission &&
                    x.Issuer == "LOCAL AUTHORITY"
                ).Select(x => x.Value);

                // Autoriza se alguma role do usuário possir a permissão requerida
                if (rolePermissions.Any())
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}