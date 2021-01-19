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
        public async Task<IActionResult> R202()
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
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .ThenInclude(x => x.Distrito)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .ToListAsync();

            var temas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema).Distinct().ToList();
            var distritos = respostas.Select(x => x.Aluno.Turma.Sala.Escola.Distrito).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r202ViewModel = new R202ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var distritosViewModel = new List<DistritoViewModel>();
            var temasViewModel = new List<TemaViewModel>();
            foreach (var distrito in distritos)
            {
                foreach (var tema in temas)
                {
                    var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.Escola.DistritoId == distrito.Id).Distinct().Count();

                    var qtdQuestoes = respostas
                        .Select(x => x.Alternativa.Questao)
                        .Where(x => x.Descritor.TemaId == tema.Id)
                        .Distinct()
                        .Count();

                    var qtdRespostasCorretas = respostas
                        .Where(x => x.Aluno.Turma.Sala.Escola.DistritoId == distrito.Id)
                        .Where(x => x.Alternativa.Questao.Descritor.TemaId == tema.Id)
                        .Select(x => x.Alternativa)
                        .Count(x => x.Correta);

                    var distritoViewModel = new DistritoViewModel
                    {
                        Distrito = distrito,
                        Tema = tema,
                        QtdAlunos = qtdAlunos
                    };
                    distritosViewModel.Add(distritoViewModel);

                    var temaViewModel = new TemaViewModel
                    {
                        Tema = tema,
                        Distrito = distrito,
                        QtdQuestoes = qtdQuestoes,
                        QtdRespostasCorretas = qtdRespostasCorretas
                    };
                    temasViewModel.Add(temaViewModel);

                    r202ViewModel.ResultadoDistritosViewModel.Add(new ResultadoDistritoViewModel
                    {
                        DistritoViewModel = distritoViewModel,
                        TemaViewModel = temaViewModel
                    });
                }
            }

            r202ViewModel.DistritosViewModel = distritosViewModel;
            r202ViewModel.TemasViewModel = temasViewModel;

            return View(r202ViewModel);
        }
    }
}