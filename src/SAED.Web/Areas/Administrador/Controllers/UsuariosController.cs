using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Extensions;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Services;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserService _userService;

        public UsuariosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            UserService userService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }

        [Authorize(Permissions.Usuarios.View)]
        public async Task<IActionResult> Index()
        {
            if (_context.Users is null)
            {
                return View();
            }

            return View(await _context.Users?.ToListAsync());
        }

        [Authorize(Permissions.Usuarios.Create)]
        public IActionResult Create()
        {
            ViewBag.AllRoles = _context.Roles.Select(x => x.Name).ToList();
            ViewBag.AllPermissions = typeof(Permissions).GetAllPublicConstantValues<string>().ToList();

            return View();
        }

        [Authorize(Permissions.Usuarios.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel viewModel)
        {
            IEnumerable<IdentityError> errors = await _userService.ValidatePasswordAsync(viewModel.Password);

            if (errors.Any())
            {
                return RedirectToAction(nameof(Create));
            }

            ApplicationUser user =
                new ApplicationUser {UserName = viewModel.Usuario.UserName, Email = viewModel.Usuario.Email};
            IdentityResult result = await _userManager.CreateAsync(user, viewModel.Password);

            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(Create));
            }

            if (viewModel.RolesEscolhidas is not null)
            {
                await _userManager.AddToRolesAsync(user, viewModel.RolesEscolhidas);
            }

            if (viewModel.PermissoesEscolhidas is not null)
            {
                List<Claim> claims = viewModel.PermissoesEscolhidas
                    .Select(x => new Claim(CustomClaimTypes.Permissions, x))
                    .ToList();
                await _userManager.AddClaimsAsync(user, claims);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Usuarios.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            IQueryable<IdentityUserRole<int>> userRoles = _context.UserRoles.Where(x => x.UserId == user.Id);
            List<string> roles = _context.Roles
                .Where(role => userRoles.Select(userRole => userRole.RoleId).Contains(role.Id))
                .Select(x => x.Name).ToList();
            ViewBag.AllRoles = _context.Roles.Where(role => !roles.Contains(role.Name)).Select(role => role.Name)
                .ToList();

            IQueryable<string> userPermissions =
                _context.UserClaims.Where(x => x.UserId == user.Id).Select(x => x.ClaimValue);
            List<string> permissions = typeof(Permissions).GetAllPublicConstantValues<string>()
                .Where(permission => userPermissions.Contains(permission)).ToList();
            ViewBag.AllPermissions = typeof(Permissions).GetAllPublicConstantValues<string>()
                .Where(permission => !permissions.Contains(permission)).ToList();

            return View(new UsuarioViewModel
            {
                Usuario = user, RolesEscolhidas = roles, PermissoesEscolhidas = permissions
            });
        }

        [Authorize(Permissions.Usuarios.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (viewModel.Password is not null)
            {
                IEnumerable<IdentityError> errors = await _userService.ValidatePasswordAsync(viewModel.Password);

                if (errors.Any())
                {
                    return RedirectToAction(nameof(Edit));
                }
            }

            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(x => x.Id == viewModel.Usuario.Id);

            if (user.Email != viewModel.Usuario.Email)
            {
                await _userManager.SetEmailAsync(user, viewModel.Usuario.Email);
            }

            if (user.UserName != viewModel.Usuario.UserName)
            {
                await _userManager.SetUserNameAsync(user, viewModel.Usuario.UserName);
            }

            if (viewModel.Password is not null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, viewModel.Password);
            }

            await _userManager.UpdateAsync(user);

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, userClaims);

            if (viewModel.RolesEscolhidas is not null)
            {
                await _userManager.AddToRolesAsync(user, viewModel.RolesEscolhidas);
            }

            if (viewModel.PermissoesEscolhidas is not null)
            {
                List<Claim> claims = viewModel.PermissoesEscolhidas
                    .Select(x => new Claim(CustomClaimTypes.Permissions, x))
                    .ToList();
                await _userManager.AddClaimsAsync(user, claims);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Usuarios.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return RedirectToAction(nameof(Index));
            }

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}