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
        public async Task<IActionResult> R103()
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
                .ThenInclude(x => x.Distrito)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .ToListAsync();

            var disciplinas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema.Disciplina).Distinct().ToList();
            var distritos = respostas.Select(x => x.Aluno.Turma.Sala.Escola.Distrito).Distinct().ToList();
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r103ViewModel = new R103ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var distritosEtapasViewModel = new List<DistritoEtapaViewModel>();
            var disciplinasViewModel = new List<DisciplinaViewModel>();
            foreach (var distrito in distritos)
            {
                foreach (var etapa in etapas)
                {
                    foreach (var disciplina in disciplinas)
                    {
                        var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.Escola.DistritoId == distrito.Id).Distinct().Count();

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

                        var distritoEtapaViewModel = new DistritoEtapaViewModel
                        {
                            Distrito = distrito,
                            Etapa = etapa,
                            Disciplina = disciplina,
                            QtdAlunos = qtdAlunos
                        };
                        distritosEtapasViewModel.Add(distritoEtapaViewModel);

                        var disciplinaViewModel = new DisciplinaViewModel
                        {
                            Disciplina = disciplina,
                            Distrito = distrito,
                            QtdQuestoesDisciplina = qtdQuestoesDisciplina,
                            QtdRespostasCorretas = qtdRespostasCorretas
                        };
                        disciplinasViewModel.Add(disciplinaViewModel);

                        r103ViewModel.ResultadoDistritosEtapasViewModel.Add(new ResultadoDistritoEtapaViewModel
                        {
                            DistritoEtapaViewModel = distritoEtapaViewModel,
                            DisciplinaViewModel = disciplinaViewModel,
                            QtdRespostasCorretas = disciplinaViewModel.QtdRespostasCorretas,
                            QtdQuestoes = disciplinaViewModel.QtdQuestoesDisciplina,
                            QtdAlunos = distritoEtapaViewModel.QtdAlunos
                        });
                    }
                }
            }

            r103ViewModel.DistritosEtapasViewModel = distritosEtapasViewModel;
            r103ViewModel.DisciplinasViewModel = disciplinasViewModel;

            return View(r103ViewModel);
        }
    }
}