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
        public async Task<IActionResult> R205()
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

            var temas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema).Distinct().ToList();
            var escolas = respostas.Select(x => x.Aluno.Turma.Sala.Escola).Distinct().ToList();
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r205ViewModel = new R205ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var escolasEtapasViewModel = new List<EscolaEtapaViewModel>();
            var temasViewModel = new List<TemaViewModel>();
            foreach (var escola in escolas)
            {
                foreach (var etapa in etapas)
                {
                    foreach (var tema in temas)
                    {
                        var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.EscolaId == escola.Id).Distinct().Count();

                        var qtdQuestoes = respostas
                            .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                            .Where(x => x.Alternativa.Questao.Descritor.TemaId == tema.Id)
                            .Select(x => x.Alternativa.Questao)
                            .Distinct()
                            .Count();

                        var qtdRespostasCorretas = respostas
                            .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                            .Where(x => x.Alternativa.Questao.Descritor.TemaId == tema.Id)
                            .Select(x => x.Alternativa)
                            .Count(x => x.Correta);

                        var escolaEtapaViewModel = new EscolaEtapaViewModel
                        {
                            Escola = escola,
                            Etapa = etapa,
                            Tema = tema,
                            QtdAlunos = qtdAlunos
                        };
                        escolasEtapasViewModel.Add(escolaEtapaViewModel);

                        var temaViewModel = new TemaViewModel
                        {
                            Tema = tema,
                            Escola = escola,
                            QtdQuestoes = qtdQuestoes,
                            QtdRespostasCorretas = qtdRespostasCorretas
                        };
                        temasViewModel.Add(temaViewModel);

                        r205ViewModel.ResultadoEscolasEtapasViewModel.Add(new ResultadoEscolaEtapaViewModel
                        {
                            EscolaEtapaViewModel = escolaEtapaViewModel,
                            TemaViewModel = temaViewModel
                        });
                    }
                }
            }

            r205ViewModel.EscolasEtapasViewModel = escolasEtapasViewModel;
            r205ViewModel.TemasViewModel = temasViewModel;

            return View(r205ViewModel);
        }
    }
}