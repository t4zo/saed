using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Identity;
using System.Threading.Tasks;

namespace SAED.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AvaliacoesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAsyncRepository<Avaliacao> _avalicaoRepository;

        public AvaliacoesController(UserManager<ApplicationUser> userManager, IAsyncRepository<Avaliacao> avalicaoRepository)
        {
            _userManager = userManager;
            _avalicaoRepository = avalicaoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _avalicaoRepository.ListAllAsync();
            return View(new SelectList(avaliacoes, "Id", "Codigo"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var avaliacao = await _avalicaoRepository.GetByIdAsync(id);
            return View(avaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            //var avaliacao = await _avalicaoRepository.GetByIdAsync(id);

            //HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
            //HttpContext.Session.SetString("avaliacao", JsonConvert.SerializeObject(avaliacao));

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains(AuthorizationConstants.Roles.Superuser) || roles.Contains(AuthorizationConstants.Roles.Administrador))
            {
                return Redirect(Url.RouteUrl(new { controller = "Dashboard", action = "Index", area = "Administrador" }));
            }

            return Redirect(Url.RouteUrl(new { controller = "Alunos", action = "Index", area = "Exame" }));
        }
    }
}
