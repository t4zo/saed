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
        public async Task<IActionResult> R105()
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
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r105ViewModel = new R105ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var escolasEtapasViewModel = new List<EscolaEtapaViewModel>();
            var disciplinasViewModel = new List<DisciplinaViewModel>();
            foreach (var escola in escolas)
            {
                foreach (var etapa in etapas)
                {
                    foreach (var disciplina in disciplinas)
                    {
                        var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.EscolaId == escola.Id).Distinct().Count();

                        var qtdQuestoesDisciplina = respostas
                            .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                            .Where(x => x.Alternativa.Questao.Descritor.Tema.DisciplinaId == disciplina.Id)
                            .Select(x => x.Alternativa.Questao)
                            .Distinct()
                            .Count();

                        var qtdRespostasCorretas = respostas
                            .Select(x => x.Alternativa)
                            .Where(x => x.Questao.Descritor.Tema.DisciplinaId == disciplina.Id)
                            .Count(x => x.Correta);

                        var escolaEtapaViewModel = new EscolaEtapaViewModel
                        {
                            Escola = escola,
                            Etapa = etapa,
                            Disciplina = disciplina,
                            QtdAlunos = qtdAlunos
                        };
                        escolasEtapasViewModel.Add(escolaEtapaViewModel);

                        var disciplinaViewModel = new DisciplinaViewModel
                        {
                            Disciplina = disciplina,
                            Escola = escola,
                            QtdQuestoesDisciplina = qtdQuestoesDisciplina,
                            QtdRespostasCorretas = qtdRespostasCorretas
                        };
                        disciplinasViewModel.Add(disciplinaViewModel);

                        r105ViewModel.ResultadoEscolasEtapasViewModel.Add(new ResultadoEscolaEtapaViewModel
                        {
                            EscolaEtapaViewModel = escolaEtapaViewModel,
                            DisciplinaViewModel = disciplinaViewModel,
                            QtdRespostasCorretas = disciplinaViewModel.QtdRespostasCorretas,
                            QtdQuestoes = disciplinaViewModel.QtdQuestoesDisciplina,
                            QtdAlunos = escolaEtapaViewModel.QtdAlunos
                        });
                    }
                }
            }

            r105ViewModel.EscolasEtapasViewModel = escolasEtapasViewModel;
            r105ViewModel.DisciplinasViewModel = disciplinasViewModel;

            return View(r105ViewModel);
        }
    }
}