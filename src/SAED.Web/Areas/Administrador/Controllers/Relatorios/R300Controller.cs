using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Web.Areas.Administrador.ViewModels.Relatorios;
using SAED.Web.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    public partial class RelatoriosController
    {
        public async Task<IActionResult> R300()
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
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .ToListAsync();

            var descritores = respostas.Select(x => x.Alternativa.Questao.Descritor).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r300ViewModel = new R300ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            foreach (var descritor in descritores)
            {
                var qtdAlunos = respostas.Select(x => x.Aluno).Distinct().Count();

                var qtdQuestoes = respostas
                    .Where(x => x.Alternativa.Questao.DescritorId == descritor.Id)
                    .Select(x => x.Alternativa.Questao)
                    .Distinct()
                    .Count();

                var qtdRespostasCorretas = respostas
                    .Select(x => x.Alternativa)
                    .Where(x => x.Questao.DescritorId == descritor.Id)
                    .Count(x => x.Correta);

                r300ViewModel.ResultadoMunicipioViewModel.Add(new ResultadoMunicipioViewModel
                {
                    Descritor = descritor,
                    QtdQuestoesDisciplina = qtdQuestoes,
                    QtdRespostasCorretas = qtdRespostasCorretas,
                    QtdQuestoes = qtdQuestoes,
                    QtdAlunos = qtdAlunos
                });
            }

            return View(r300ViewModel);
        }
    }
}