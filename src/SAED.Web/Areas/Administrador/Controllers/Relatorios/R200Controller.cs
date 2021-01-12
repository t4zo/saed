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
        public async Task<IActionResult> R200()
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

            var disciplinas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema.Disciplina).Distinct().ToList();
            var temas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r200ViewModel = new R200ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            foreach (var tema in temas)
            {
                var qtdAlunos = respostas.Select(x => x.Aluno).Distinct().Count();

                var qtdQuestoesTema = respostas
                    .Select(x => x.Alternativa.Questao)
                    .Where(x => x.Descritor.TemaId == tema.Id)
                    .Distinct()
                    .Count();

                var qtdRespostasCorretas = respostas
                    .Select(x => x.Alternativa)
                    .Where(x => x.Questao.Descritor.TemaId == tema.Id)
                    .Count(x => x.Correta);

                r200ViewModel.ResultadoMunicipioViewModel.Add(new ResultadoMunicipioViewModel
                {
                    Tema = tema,
                    QtdQuestoesTema = qtdQuestoesTema,
                    QtdRespostasCorretas = qtdRespostasCorretas,
                    QtdQuestoes = qtdQuestoesTema,
                    QtdAlunos = qtdAlunos
                });
            }

            return View(r200ViewModel);
        }
    }
}