using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Aplicador.ViewModels;
using SAED.Web.Extensions;

namespace SAED.Web.Areas.Aplicador.Controllers
{
    [Area(AuthorizationConstants.Areas.Aplicador)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.DashboardAplicador.View)]
        public async Task<IActionResult> Index()
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());
            
            var questoes = await _context.Questoes
                .AsNoTracking()
                .Include(x => x.Descritor)
                .ThenInclude(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .Where(x => x.Avaliacoes.Any(y => y.Id == avaliacao.Id))
                .ToListAsync();

            foreach (var questao in questoes)
            {
                questao.Descritor.Questoes = null;
                questao.Descritor.Tema.Descritores = null;
                questao.Descritor.Tema.Disciplina.Temas = null;
            }
            
            var dashboardAplicadorViewModel = HttpContext.Session.Get<DashboardAplicadorViewModel>("alunoMetadata");

            dashboardAplicadorViewModel.Questoes = questoes;

            return View(dashboardAplicadorViewModel);
        }
    }
}