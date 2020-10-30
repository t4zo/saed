using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

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
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            HttpContext.Session.Set("avaliacao", avaliacao);

            if (User.IsInRole(Roles.Superuser) || User.IsInRole(Roles.Administrador))
            {
                return Redirect(Url.RouteUrl(new { controller = "Dashboard", action = "Index", area = "Administrador" }));
            }

            //return Redirect(Url.RouteUrl(new { controller = "Alunos", action = "Index", area = "Exame" }));
            return Redirect(Url.RouteUrl(new { controller = "Dashboard", action = "Index", area = "Administrador" }));
        }
    }
}
