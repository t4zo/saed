using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using System.Threading.Tasks;

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
            var avaliacoes = await _avaliacaoRepository.ListAllAsync();
            return View(new SelectList(avaliacoes, "Id", "Codigo"));
        }

        [HttpPost]
        public IActionResult Index(int? id)
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var avaliacao = await _avalicaoRepository.GetByIdAsync(id);

            //HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
            //HttpContext.Session.SetString("avaliacao", JsonConvert.SerializeObject(avaliacao));

            //var roles = await _userManager.GetRolesAsync(user);

            //if (roles.Contains(AuthorizationConstants.Roles.Superuser) || roles.Contains(AuthorizationConstants.Roles.Administrador))
            //{
            //    return Redirect(Url.RouteUrl(new { controller = "Dashboard", action = "Index", area = "Administrador" }));
            //}

            //return Redirect(Url.RouteUrl(new { controller = "Alunos", action = "Index", area = "Exame" }));
            return Redirect(Url.RouteUrl(new { controller = "Dashboard", action = "Index", area = "Administrador" }));
        }
    }
}
