using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Extensions;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Areas.Administrador.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuariosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Permissions.Usuarios.View)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
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
            var usuario = new ApplicationUser { UserName = viewModel.Usuario.UserName, Email = viewModel.Usuario.Email };

            var createResult = await _userManager.CreateAsync(usuario);
            var addPasswordResult = await _userManager.AddPasswordAsync(usuario, viewModel.Password);

            if (!createResult.Succeeded || !addPasswordResult.Succeeded)
            {
                ViewBag.AllRoles = _context.Roles.Select(x => x.Name).ToList();
                ViewBag.AllPermissions = typeof(Permissions).GetAllPublicConstantValues<string>().ToList();

                return View();
            }

            if (viewModel.RolesEscolhidas != null)
            {
                await _userManager.AddToRolesAsync(usuario, viewModel.RolesEscolhidas);
            }

            if (viewModel.RolesEscolhidas != null)
            {
                foreach (var permissaoEscolhida in viewModel.PermissoesEscolhidas)
                {
                    await _userManager.AddClaimAsync(usuario, new Claim(CustomClaimTypes.Permission, permissaoEscolhida));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Usuarios.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (usuario is null)
            {
                return NotFound();
            }

            var userRoles = _context.UserRoles.Where(x => x.UserId == usuario.Id);
            var roles = _context.Roles.Where(role => userRoles.Select(userRole => userRole.RoleId).Contains(role.Id)).Select(x => x.Name).ToList();
            ViewBag.AllRoles = _context.Roles.Where(role => !roles.Contains(role.Name)).Select(role => role.Name).ToList();

            var userPermissions = _context.UserClaims.Where(x => x.UserId == usuario.Id).Select(x => x.ClaimValue);
            var permissions = typeof(Permissions).GetAllPublicConstantValues<string>().Where(permission => userPermissions.Contains(permission)).ToList();
            ViewBag.AllPermissions = typeof(Permissions).GetAllPublicConstantValues<string>().Where(permission => !permissions.Contains(permission)).ToList();

            return View(new UsuarioViewModel { Usuario = usuario, RolesEscolhidas = roles, PermissoesEscolhidas = permissions });
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

            var usuario = await _context.Users.FirstOrDefaultAsync(x => x.Id == viewModel.Usuario.Id);
            usuario.UserName = viewModel.Usuario.UserName;
            usuario.NormalizedUserName = viewModel.Usuario.UserName.Trim().ToUpper();
            usuario.Email = viewModel.Usuario.Email;
            usuario.NormalizedEmail = viewModel.Usuario.Email.Trim().ToUpper();

            if (viewModel.Password != null)
            {
                usuario.PasswordHash = _userManager.PasswordHasher.HashPassword(usuario, viewModel.Password);
                await _userManager.UpdateAsync(usuario);
            }

            var userRoles = await _userManager.GetRolesAsync(usuario);
            var resultUserRoles = await _userManager.RemoveFromRolesAsync(usuario, userRoles);

            var userClaims = await _userManager.GetClaimsAsync(usuario);
            var resultUserClaims = await _userManager.RemoveClaimsAsync(usuario, userClaims);

            if (!resultUserRoles.Succeeded || !resultUserClaims.Succeeded)
            {
                return RedirectToAction(nameof(Edit));
            }

            if (viewModel.RolesEscolhidas != null)
            {
                await _userManager.AddToRolesAsync(usuario, viewModel.RolesEscolhidas);
            }

            if (viewModel.PermissoesEscolhidas != null)
            {
                foreach (var permissaoEscolhida in viewModel.PermissoesEscolhidas)
                {
                    await _userManager.AddClaimAsync(usuario, new Claim(CustomClaimTypes.Permission, permissaoEscolhida));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Usuarios.Delete)]
        public IActionResult Delete()
        {
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Usuarios.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return RedirectToAction(nameof(Index));
            }

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
