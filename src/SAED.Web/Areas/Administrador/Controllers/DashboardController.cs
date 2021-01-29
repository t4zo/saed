using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Dashboard.View)]
        public async Task<IActionResult> Index()
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);

            var respostas = await _context.RespostaAlunos
                .AsNoTracking()
                .Include(x => x.Alternativa)
                .ThenInclude(x => x.Questao)
                .ThenInclude(x => x.Descritor)
                .ThenInclude(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .Include(x => x.Aluno)
                .ThenInclude(x => x.Turma)
                .ThenInclude(x => x.Etapa)
                .Include(x => x.Aluno)
                .ThenInclude(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .ToListAsync();

            var qtdAlunos = respostas.Select(x => x.Aluno).Distinct().Count();
            var qtdAlunosTotal = await _context.Alunos.AsNoTracking().CountAsync();

            var qtdEscolas = respostas.Select(x => x.Aluno.Turma.Sala.Escola).Distinct().Count();
            var qtdEtapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().Count();

            var qtdDisciplinas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema.Disciplina).Distinct().Count();
            var qtdTemas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema).Distinct().Count();
            var qtdDescritores = respostas.Select(x => x.Alternativa.Questao.Descritor).Distinct().Count();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var dashboardViewModel = new DashboardViewModel
            {
                QtdAlunos = qtdAlunos,
                QtdAlunosTotal = qtdAlunosTotal,
                QtdEscolas = qtdEscolas,
                QtdEtapas = qtdEtapas,
                QtdDisciplinas = qtdDisciplinas,
                QtdTemas = qtdTemas,
                QtdDescritores = qtdDescritores,
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            return View(dashboardViewModel);
        }
    }
}