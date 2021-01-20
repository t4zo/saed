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
        public async Task<IActionResult> R301()
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
            var etapas = respostas.Select(x => x.Aluno.Turma.Etapa).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r301ViewModel = new R301ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var etapasViewModel = new List<EtapaViewModel>();
            var descritoresViewModel = new List<DescritorViewModel>();
            foreach (var etapa in etapas)
            {
                foreach (var descritor in descritores)
                {
                    var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.EtapaId == etapa.Id).Distinct().Count();

                    var qtdQuestoes = respostas
                        .Select(x => x.Alternativa.Questao)
                        .Where(x => x.DescritorId == descritor.Id)
                        .Distinct()
                        .Count();

                    var qtdRespostasCorretas = respostas
                        .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                        .Where(x => x.Alternativa.Questao.DescritorId == descritor.Id)
                        .Select(x => x.Alternativa)
                        .Count(x => x.Correta);

                    var etapaViewModel = new EtapaViewModel
                    {
                        Etapa = etapa,
                        Descritor = descritor,
                        QtdAlunos = qtdAlunos
                    };
                    etapasViewModel.Add(etapaViewModel);

                    var descritorViewModel = new DescritorViewModel
                    {
                        Descritor = descritor,
                        Etapa = etapa,
                        QtdQuestoes = qtdQuestoes,
                        QtdRespostasCorretas = qtdRespostasCorretas
                    };
                    descritoresViewModel.Add(descritorViewModel);

                    r301ViewModel.ResultadoEtapasViewModel.Add(new ResultadoEtapaViewModel
                    {
                        EtapaViewModel = etapaViewModel,
                        DescritorViewModel = descritorViewModel
                    });
                }
            }

            r301ViewModel.EtapasViewModel = etapasViewModel;
            r301ViewModel.DescritoresViewModel = descritoresViewModel;

            return View(r301ViewModel);
        }
    }
}