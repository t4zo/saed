using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Web.Areas.Administrador.ViewModels.Relatorios;
using SAED.Web.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    public partial class RelatoriosController
    {
        public async Task<IActionResult> R104()
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

            var disciplinas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema.Disciplina).Distinct().ToList();
            var escolas = respostas.Select(x => x.Aluno.Turma.Sala.Escola).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r104ViewModel = new R104ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var escolasViewModel = new List<EscolaViewModel>();
            var disciplinasViewModel = new List<DisciplinaViewModel>();
            foreach (var escola in escolas)
            {
                foreach (var disciplina in disciplinas)
                {
                    var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.EscolaId == escola.Id).Distinct().Count();

                    var qtdQuestoesDisciplina = respostas
                        .Select(x => x.Alternativa.Questao)
                        .Where(x => x.Descritor.Tema.DisciplinaId == disciplina.Id)
                        .Distinct()
                        .Count();

                    var qtdRespostasCorretas = respostas
                        .Select(x => x.Alternativa)
                        .Where(x => x.Questao.Descritor.Tema.DisciplinaId == disciplina.Id)
                        .Count(x => x.Correta);

                    var escolaViewModel = new EscolaViewModel
                    {
                        Escola = escola,
                        DisciplinaId = disciplina.Id,
                        QtdAlunos = qtdAlunos
                    };
                    escolasViewModel.Add(escolaViewModel);

                    var disciplinaViewModel = new DisciplinaViewModel
                    {
                        Disciplina = disciplina,
                        EscolaId = escola.Id,
                        QtdQuestoesDisciplina = qtdQuestoesDisciplina,
                        QtdRespostasCorretas = qtdRespostasCorretas
                    };
                    disciplinasViewModel.Add(disciplinaViewModel);

                    r104ViewModel.ResultadoEscolasViewModel.Add(new ResultadoEscolaViewModel
                    {
                        EscolaViewModel = escolaViewModel,
                        DisciplinaViewModel = disciplinaViewModel,
                        QtdRespostasCorretas = disciplinaViewModel.QtdRespostasCorretas,
                        QtdQuestoes = disciplinaViewModel.QtdQuestoesDisciplina,
                        QtdAlunos = escolaViewModel.QtdAlunos
                    });
                }
            }

            r104ViewModel.EscolasViewModel = escolasViewModel;
            r104ViewModel.DisciplinasViewModel = disciplinasViewModel;

            return View(r104ViewModel);
        }
    }
}