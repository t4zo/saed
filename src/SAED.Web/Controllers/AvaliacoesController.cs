using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAED.Core.Constants;
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

        public IActionResult Index()
        {
            ViewBag.Avaliacoes = new SelectList(_context.Avaliacoes, "Id", "Codigo");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            HttpContext.Session.Set(SessionConstants.Avaliacao, avaliacao);

            if (User.IsInRole(Roles.Superuser) || User.IsInRole(Roles.Administrador))
            {
                return Redirect($"{AuthorizationConstants.Areas.Administrador}/Dashboard".ToLower());
            }

            if (User.IsInRole(Roles.Aplicador))
            {
                return Redirect($"{AuthorizationConstants.Areas.Aplicador}/Selecao".ToLower());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}