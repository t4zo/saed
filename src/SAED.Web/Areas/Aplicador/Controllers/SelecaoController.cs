using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Aplicador.ViewModels;
using SAED.Web.Extensions;

namespace SAED.Web.Areas.Aplicador.Controllers
{
    [Area(AuthorizationConstants.Areas.Aplicador)]
    public class SelecaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelecaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Selecao.View)]
        public IActionResult Index()
        {
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Selecao.View)]
        [HttpPost]
        public async Task<IActionResult> Index(DashboardAplicadorViewModel dashboardAplicadorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            dashboardAplicadorViewModel.Escola = await _context.Escolas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dashboardAplicadorViewModel.EscolaId);

            dashboardAplicadorViewModel.Etapa = await _context.Etapas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dashboardAplicadorViewModel.EtapaId);

            dashboardAplicadorViewModel.Turma = await _context.Turmas
                .AsNoTracking().Include(x => x.Turno)
                .FirstOrDefaultAsync(x => x.Id == dashboardAplicadorViewModel.TurmaId);

            dashboardAplicadorViewModel.Aluno = await _context.Alunos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dashboardAplicadorViewModel.AlunoId);

            dashboardAplicadorViewModel.Aluno.Turma = null;
            dashboardAplicadorViewModel.Etapa.Turmas = null;
            dashboardAplicadorViewModel.Turma.Alunos = null;
            dashboardAplicadorViewModel.Turma.Etapa = null;
            dashboardAplicadorViewModel.Turma.Turno.Turmas = null;

            HttpContext.Session.Set("alunoMetadata", dashboardAplicadorViewModel);

            return RedirectToAction(nameof(Index), "Dashboard");
        }
    }
}