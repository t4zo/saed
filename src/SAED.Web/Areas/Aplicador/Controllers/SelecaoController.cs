using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Aplicador.ViewModels;
using SAED.Web.Extensions;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);

            var allEtapas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Etapa)
                .ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Sala)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Etapa)
                .ToListAsync();

            var etapas = allEtapas.Distinct().ToList();

            HttpContext.Session.Clear();
            HttpContext.Session.Set(SessionConstants.Avaliacao, avaliacao);

            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome");
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
                .Where(x => x.Id == dashboardAplicadorViewModel.EtapaId)
                .Select(x => new Etapa
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Normativa = x.Normativa,
                    SegmentoId = x.SegmentoId,
                    Segmento = x.Segmento
                })
                .FirstOrDefaultAsync();

            dashboardAplicadorViewModel.Turma = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Turno)
                .Where(x => x.Id == dashboardAplicadorViewModel.TurmaId)
                .Select(x => new Turma
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    EtapaId = x.EtapaId,
                    FormaId = x.FormaId,
                    SalaId = x.SalaId,
                    TurnoId = x.TurnoId,
                    Turno = x.Turno,
                    Extinta = x.Extinta,
                    QtdAlunos = x.QtdAlunos
                })
                .FirstOrDefaultAsync();

            dashboardAplicadorViewModel.Aluno = await _context.Alunos
                .AsNoTracking()
                .Where(x => x.Id == dashboardAplicadorViewModel.AlunoId)
                .Select(x => new Aluno
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Nascimento = x.Nascimento,
                    TurmaId = x.TurmaId
                })
                .FirstOrDefaultAsync();

            HttpContext.Session.Set(SessionConstants.Aluno, dashboardAplicadorViewModel);

            return RedirectToAction(nameof(Index), "Dashboard");
        }
    }
}