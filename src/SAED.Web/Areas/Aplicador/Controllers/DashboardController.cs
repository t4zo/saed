using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);
            var dashboardAplicadorViewModel = HttpContext.Session.Get<DashboardAplicadorViewModel>(SessionConstants.Aluno);
            var respostasViewModel = HttpContext.Session.Get<RespostasViewModel>(SessionConstants.RespostasAluno);

            var questoes = await _context.Questoes
                .AsNoTracking()
                .Include(x => x.Descritor)
                .ThenInclude(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .Include(x => x.Alternativas)
                .Where(x => x.Avaliacoes.Any(y => y.Id == avaliacao.Id))
                .Where(x => x.EtapaId == dashboardAplicadorViewModel.EtapaId)
                .ToListAsync();

            foreach (var questao in questoes)
            {
                questao.ClearReferenceCycle();

                foreach (var alternativa in questao.Alternativas)
                {
                    alternativa.ClearReferenceCycle();
                }
            }

            HttpContext.Session.Set(SessionConstants.Questoes, questoes);

            dashboardAplicadorViewModel.Questoes = questoes;
            dashboardAplicadorViewModel.RespostasViewModel = respostasViewModel;

            return View(dashboardAplicadorViewModel);
        }

        [Authorize(AuthorizationConstants.Permissions.DashboardAplicador.View)]
        [HttpPost]
        public async Task<IActionResult> Index(string _)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);
            var dashboardAplicadorViewModel = HttpContext.Session.Get<DashboardAplicadorViewModel>(SessionConstants.Aluno);
            var respostasViewModel = HttpContext.Session.Get<RespostasViewModel>(SessionConstants.RespostasAluno);

            var questoes = await _context.Questoes
                .AsNoTracking()
                .Include(x => x.Descritor)
                .ThenInclude(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .Include(x => x.Alternativas)
                .Where(x => x.Avaliacoes.Any(y => y.Id == avaliacao.Id))
                .Where(x => x.EtapaId == dashboardAplicadorViewModel.EtapaId)
                .ToListAsync();

            foreach (var questao in questoes)
            {
                questao.ClearReferenceCycle();

                foreach (var alternativa in questao.Alternativas)
                {
                    alternativa.ClearReferenceCycle();
                }
            }

            HttpContext.Session.Set(SessionConstants.Questoes, questoes);

            if (dashboardAplicadorViewModel.AlunoId != respostasViewModel.AlunoId || avaliacao.Id != respostasViewModel.AvaliacaoId)
            {
                return BadRequest();
            }

            dashboardAplicadorViewModel.Questoes = questoes;
            dashboardAplicadorViewModel.RespostasViewModel = respostasViewModel;

            foreach (var respostaViewModel in dashboardAplicadorViewModel.RespostasViewModel.Respostas)
            {
                await _context.RespostaAlunos.AddAsync(new RespostaAluno
                {
                    AvaliacaoId = avaliacao.Id,
                    AlternativaId = respostaViewModel.AlternativaEscolhida.Id,
                    AlunoId = dashboardAplicadorViewModel.AlunoId
                });
            }

            await _context.SaveChangesAsync();

            return Redirect("/avaliacoes");
        }
    }
}