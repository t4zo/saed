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
        public async Task<IActionResult> R305()
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

            var descritores = respostas.Select(x => x.Alternativa.Questao.Descritor).Distinct().ToList();
            var escolas = respostas.Select(x => x.Aluno.Turma.Sala.Escola).Distinct().ToList();
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r305ViewModel = new R305ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var escolasEtapasViewModel = new List<EscolaEtapaViewModel>();
            var descritoresViewModel = new List<DescritorViewModel>();
            foreach (var escola in escolas)
            {
                foreach (var etapa in etapas)
                {
                    foreach (var descritor in descritores)
                    {
                        var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.EscolaId == escola.Id).Distinct().Count();

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

                        var escolaEtapaViewModel = new EscolaEtapaViewModel
                        {
                            Escola = escola,
                            Etapa = etapa,
                            Descritor = descritor,
                            QtdAlunos = qtdAlunos
                        };
                        escolasEtapasViewModel.Add(escolaEtapaViewModel);

                        var descritorViewModel = new DescritorViewModel
                        {
                            Descritor = descritor,
                            Escola = escola,
                            QtdQuestoes = qtdQuestoes,
                            QtdRespostasCorretas = qtdRespostasCorretas
                        };
                        descritoresViewModel.Add(descritorViewModel);

                        r305ViewModel.ResultadoEscolasEtapasViewModel.Add(new ResultadoEscolaEtapaViewModel
                        {
                            EscolaEtapaViewModel = escolaEtapaViewModel,
                            DescritorViewModel = descritorViewModel
                        });
                    }
                }
            }

            r305ViewModel.EscolasEtapasViewModel = escolasEtapasViewModel;
            r305ViewModel.DescritoresViewModel = descritoresViewModel;

            return View(r305ViewModel);
        }
    }
}