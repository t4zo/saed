using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.Web.Extensions;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Controllers
{
    [Authorize(AuthorizationConstants.Permissions.Avaliacoes.View)]
    public class AvaliacoesController : Controller
    {
        private readonly IAsyncRepository<Avaliacao> _avaliacaoRepository;

        public AvaliacoesController(IAsyncRepository<Avaliacao> avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Avaliacoes = new SelectList(await _avaliacaoRepository.ListAllAsync(), "Id", "Codigo");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);

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
