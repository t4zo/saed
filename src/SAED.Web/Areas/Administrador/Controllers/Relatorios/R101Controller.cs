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
        public async Task<IActionResult> R101()
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
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r101ViewModel = new R101ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var etapasViewModel = new List<EtapaViewModel>();
            var disciplinasViewModel = new List<DisciplinaViewModel>();
            foreach (var etapa in etapas)
            {
                foreach (var disciplina in disciplinas)
                {
                    var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.EtapaId == etapa.Id).Distinct().Count();

                    var qtdQuestoesDisciplina = respostas
                        .Select(x => x.Alternativa.Questao)
                        .Where(x => x.Descritor.Tema.DisciplinaId == disciplina.Id)
                        .Distinct()
                        .Count();

                    var qtdRespostasCorretas = respostas
                        .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                        .Where(x => x.Alternativa.Questao.Descritor.Tema.DisciplinaId == disciplina.Id)
                        .Select(x => x.Alternativa)
                        .Count(x => x.Correta);

                    var etapaViewModel = new EtapaViewModel
                    {
                        Etapa = etapa,
                        DisciplinaId = disciplina.Id,
                        QtdAlunos = qtdAlunos
                    };
                    etapasViewModel.Add(etapaViewModel);

                    var disciplinaViewModel = new DisciplinaViewModel
                    {
                        Disciplina = disciplina,
                        EtapaId = etapa.Id,
                        QtdQuestoesDisciplina = qtdQuestoesDisciplina,
                        QtdRespostasCorretas = qtdRespostasCorretas
                    };
                    disciplinasViewModel.Add(disciplinaViewModel);

                    r101ViewModel.ResultadoEtapasViewModel.Add(new ResultadoEtapaViewModel
                    {
                        EtapaViewModel = etapaViewModel,
                        DisciplinaViewModel = disciplinaViewModel,
                        QtdRespostasCorretas = disciplinaViewModel.QtdRespostasCorretas,
                        QtdQuestoes = disciplinaViewModel.QtdQuestoesDisciplina,
                        QtdAlunos = etapaViewModel.QtdAlunos
                    });
                }
            }

            r101ViewModel.EtapasViewModel = etapasViewModel;
            r101ViewModel.DisciplinasViewModel = disciplinasViewModel;

            return View(r101ViewModel);
        }
    }
}