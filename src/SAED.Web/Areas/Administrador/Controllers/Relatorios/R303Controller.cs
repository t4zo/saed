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
        public async Task<IActionResult> R303()
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

            var descritores = respostas.Select(x => x.Alternativa.Questao.Descritor).Distinct().ToList();
            var distritos = respostas.Select(x => x.Aluno.Turma.Sala.Escola.Distrito).Distinct().ToList();
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r303ViewModel = new R303ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var distritosEtapasViewModel = new List<DistritoEtapaViewModel>();
            var descritoresViewModel = new List<DescritorViewModel>();
            foreach (var distrito in distritos)
            {
                foreach (var etapa in etapas)
                {
                    foreach (var descritor in descritores)
                    {
                        var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.Escola.DistritoId == distrito.Id).Distinct().Count();

                        var qtdQuestoes = respostas
                            .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                            .Where(x => x.Alternativa.Questao.DescritorId == descritor.Id)
                            .Select(x => x.Alternativa.Questao)
                            .Distinct()
                            .Count();

                        var qtdRespostasCorretas = respostas
                            .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                            .Where(x => x.Alternativa.Questao.DescritorId == descritor.Id)
                            .Select(x => x.Alternativa)
                            .Count(x => x.Correta);

                        var distritoEtapaViewModel = new DistritoEtapaViewModel
                        {
                            Distrito = distrito,
                            Etapa = etapa,
                            Descritor = descritor,
                            QtdAlunos = qtdAlunos
                        };
                        distritosEtapasViewModel.Add(distritoEtapaViewModel);

                        var descritorViewModel = new DescritorViewModel
                        {
                            Descritor = descritor,
                            Distrito = distrito,
                            QtdQuestoes = qtdQuestoes,
                            QtdRespostasCorretas = qtdRespostasCorretas
                        };
                        descritoresViewModel.Add(descritorViewModel);

                        r303ViewModel.ResultadoDistritosEtapasViewModel.Add(new ResultadoDistritoEtapaViewModel
                        {
                            DistritoEtapaViewModel = distritoEtapaViewModel,
                            DescritorViewModel = descritorViewModel
                        });
                    }
                }
            }

            r303ViewModel.DistritosEtapasViewModel = distritosEtapasViewModel;
            r303ViewModel.DescritoresViewModel = descritoresViewModel;

            return View(r303ViewModel);
        }
    }
}