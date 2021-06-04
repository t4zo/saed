using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Persistence.Data;
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

            ViewData["AlunoPreenchido"] = false;
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome");
            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Selecao.View)]
        [HttpGet("[area]/[controller]/{alunoId}")]
        public async Task<IActionResult> Index(int? alunoId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);

            HttpContext.Session.Clear();
            HttpContext.Session.Set(SessionConstants.Avaliacao, avaliacao);

            var aluno = await _context.Alunos
                .AsNoTracking()
                .Include(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .Include(x => x.Turma)
                .ThenInclude(x => x.Etapa)
                .FirstOrDefaultAsync(x => x.Id == alunoId);

            ViewData["AlunoPreenchido"] = true;
            ViewData["Aluno"] = aluno;

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Selecao.View)]
        [Route("[area]/[controller]")]
        [Route("[area]/[controller]/{alunoId}")]
        [HttpPost]
        public async Task<IActionResult> Index(int? alunoId, DashboardAplicadorViewModel dashboardAplicadorViewModel)
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
                    Extinta = x.Extinta
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