using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Extensions;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Extensions;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class GruposController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public GruposController(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        [Authorize(Permissions.Grupos.View)]
        public IActionResult Index()
        {
            ViewBag.Roles = _roleManager.Roles?.ToList();

            return View();
        }

        [Authorize(Permissions.Grupos.Create)]
        public IActionResult Create()
        {
            ViewBag.AllPermissions = typeof(Permissions).GetAllPublicConstantValues<string>();

            return View();
        }

        [Authorize(Permissions.Grupos.Create)]
        [HttpPost]
        public async Task<IActionResult> Create(GrupoViewModel viewModel)
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole {Name = viewModel.Nome});
            if (!result.Succeeded)
            {
                return RedirectToAction(nameof(Create));
            }

            ApplicationRole role = await _roleManager.FindByNameAsync(viewModel.Nome);

            if (viewModel.PermissoesEscolhidas is not null)
            {
                foreach (string permissaoEscolhida in viewModel.PermissoesEscolhidas)
                {
                    await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permissions, permissaoEscolhida));
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Grupos.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            ApplicationRole role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            HttpContext.Session.Set("Role", role.Name);

            List<string> roleClaims = await _context.RoleClaims.AsNoTracking().Where(x => x.RoleId == role.Id)
                .Select(x => x.ClaimValue).ToListAsync();
            ViewBag.Role = role;
            ViewBag.AllPermissions = typeof(Permissions).GetAllPublicConstantValues<string>()
                .Where(permission => !roleClaims.Contains(permission)).ToList();

            return View(new GrupoViewModel {Nome = role.Name, PermissoesEscolhidas = roleClaims});
        }

        [Authorize(Permissions.Grupos.Update)]
        [HttpPost]
        public async Task<IActionResult> Edit(GrupoViewModel viewModel)
        {
            ApplicationRole role = await _roleManager.FindByNameAsync(HttpContext.Session.Get<string>("Role"));
            //var role = await _roleManager.FindByIdAsync(Role.Id.ToString());
            //_context.Entry(role).State = EntityState.Detached;
            role.Name = viewModel.Nome;

            IList<Claim> claims = await _roleManager.GetClaimsAsync(role);

            foreach (Claim claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            if (viewModel.PermissoesEscolhidas != null)
            {
                foreach (string permissaoEscolhida in viewModel.PermissoesEscolhidas)
                {
                    await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permissions, permissaoEscolhida));
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Grupos.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationRole role = await _roleManager.FindByIdAsync(id);

            if (role is null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);

            return RedirectToAction(nameof(Index));
        }
    }
}