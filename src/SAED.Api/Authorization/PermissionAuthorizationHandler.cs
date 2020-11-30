using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Modificar completamente para validar pelo Token
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            ApplicationUser user = await _userManager.GetUserAsync(context.User);

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

            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IEnumerable<string> userPermissions = userClaims.Where(x =>
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

            IList<string> userRoleNames = await _userManager.GetRolesAsync(user);
            IQueryable<ApplicationRole> userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

            foreach (ApplicationRole role in userRoles)
            {
                IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);

                IEnumerable<string> rolePermissions = roleClaims.Where(x =>
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