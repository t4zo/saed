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
        public async Task<IActionResult> R201()
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

            var temas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema).Distinct().ToList();
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r201ViewModel = new R201ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var etapasViewModel = new List<EtapaViewModel>();
            var temasViewModel = new List<TemaViewModel>();
            foreach (var etapa in etapas)
            {
                foreach (var tema in temas)
                {
                    var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.EtapaId == etapa.Id).Distinct().Count();

                    var qtdQuestoes = respostas
                        .Select(x => x.Alternativa.Questao)
                        .Where(x => x.Descritor.TemaId == tema.Id)
                        .Distinct()
                        .Count();

                    var qtdRespostasCorretas = respostas
                        .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                        .Where(x => x.Alternativa.Questao.Descritor.TemaId == tema.Id)
                        .Select(x => x.Alternativa)
                        .Count(x => x.Correta);

                    var etapaViewModel = new EtapaViewModel
                    {
                        Etapa = etapa,
                        Tema = tema,
                        QtdAlunos = qtdAlunos
                    };
                    etapasViewModel.Add(etapaViewModel);

                    var temaViewModel = new TemaViewModel
                    {
                        Tema = tema,
                        Etapa = etapa,
                        QtdQuestoes = qtdQuestoes,
                        QtdRespostasCorretas = qtdRespostasCorretas
                    };
                    temasViewModel.Add(temaViewModel);

                    r201ViewModel.ResultadoEtapasViewModel.Add(new ResultadoEtapaViewModel
                    {
                        EtapaViewModel = etapaViewModel,
                        TemaViewModel = temaViewModel
                    });
                }
            }

            r201ViewModel.EtapasViewModel = etapasViewModel;
            r201ViewModel.TemasViewModel = temasViewModel;

            return View(r201ViewModel);
        }
    }
}