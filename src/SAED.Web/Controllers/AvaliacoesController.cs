using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Controllers
{
    [Authorize]
    public class AvaliacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Avaliacoes = new SelectList(await _context.Avaliacoes.ToListAsync(), "Id", "Codigo");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            Avaliacao avaliacao = await _context.Avaliacoes.FindAsync(id);

            HttpContext.Session.Set(nameof(Avaliacao).ToLower(), avaliacao);

            if (User.IsInRole(Roles.Superuser) || User.IsInRole(Roles.Administrador))
            {
                return Redirect(Url.RouteUrl(new {controller = "Dashboard", action = "Index", area = "Administrador"}));
            }

            if (User.IsInRole(Roles.Aplicador))
            {
                return Redirect(Url.RouteUrl(new {controller = "Selecao", action = "Index", area = "Aplicador"}));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}