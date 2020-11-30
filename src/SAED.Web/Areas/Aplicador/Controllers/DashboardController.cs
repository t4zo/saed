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
            
            var dashboardAplicadorViewModel = HttpContext.Session.Get<DashboardAplicadorViewModel>("alunoMetadata");

            dashboardAplicadorViewModel.Disciplinas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Disciplina)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Disciplina)
                .ToListAsync();
            
            return View(dashboardAplicadorViewModel);
        }
    }
}