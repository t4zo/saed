using System.Threading.Tasks;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SAED.Infrastructure.Identity;

namespace SAED.Web.Controllers
{
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
            return View(avaliacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var avaliacaoSpec = new AvaliacoesWithSpecification(id);

            var avaliacao = await _avalicaoRepository.FirstOrDefaultAsync(avaliacaoSpec);

            return View(avaliacao);

            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
            //HttpContext.Session.SetString("avaliacao", JsonConvert.SerializeObject(avaliacao));

            //var roles = await _userManager.GetRolesAsync(user);

            //if (roles.Contains(AuthorizationConstants.Roles.ADMINISTRATOR))
            //{
            //    return Redirect(Url.RouteUrl(new { controller = "dashboard", action = "index", area = "administrador" }));
            //}

            //return Redirect(Url.RouteUrl(new { controller = "alunos", action = "index", area = "exame" }));
        }
    }
}
