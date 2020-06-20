using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SAED.Infrastructure.Identity;
using System.Linq;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole<int>> _roleManager;

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var user = await _userManager.GetUserAsync(context.User);

            // Se não estiver logado retorna não autorizado
            if (user == null)
            {
                return;
            }

            // Autoriza se for Superusuário
            if (await _userManager.IsInRoleAsync(user, PermissionsAndRoles.SUPERUSER))
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
                    x.Type == CustomClaimTypes.Permission &&
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
