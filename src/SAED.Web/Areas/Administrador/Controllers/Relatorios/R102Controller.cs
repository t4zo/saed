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
        //[HttpGet("[area]/[controller]/r102")]
        //public async Task<IActionResult> R102ConsolidadoporDistrito()
        public async Task<IActionResult> R102()
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

            var disciplinas = respostas.Select(x => x.Alternativa.Questao.Descritor.Tema.Disciplina).Distinct().ToList();
            var distritos = respostas.Select(x => x.Aluno.Turma.Sala.Escola.Distrito).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r102ViewModel = new R102ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var distritosViewModel = new List<DistritoViewModel>();
            var disciplinasViewModel = new List<DisciplinaViewModel>();
            foreach (var distrito in distritos)
            {
                foreach (var disciplina in disciplinas)
                {
                    var qtdAlunos = respostas.Select(x => x.Aluno).Where(x => x.Turma.Sala.Escola.DistritoId == distrito.Id).Distinct().Count();

                    var qtdQuestoes = respostas
                        .Select(x => x.Alternativa.Questao)
                        .Where(x => x.Descritor.Tema.DisciplinaId == disciplina.Id)
                        .Distinct()
                        .Count();

                    var qtdRespostasCorretas = respostas
                        .Where(x => x.Aluno.Turma.Sala.Escola.DistritoId == distrito.Id)
                        .Where(x => x.Alternativa.Questao.Descritor.Tema.DisciplinaId == disciplina.Id)
                        .Select(x => x.Alternativa)
                        .Count(x => x.Correta);

                    var distritoViewModel = new DistritoViewModel
                    {
                        Distrito = distrito,
                        Disciplina = disciplina,
                        QtdAlunos = qtdAlunos
                    };
                    distritosViewModel.Add(distritoViewModel);

                    var disciplinaViewModel = new DisciplinaViewModel
                    {
                        Disciplina = disciplina,
                        Distrito = distrito,
                        QtdQuestoes = qtdQuestoes,
                        QtdRespostasCorretas = qtdRespostasCorretas
                    };
                    disciplinasViewModel.Add(disciplinaViewModel);

                    r102ViewModel.ResultadoDistritosViewModel.Add(new ResultadoDistritoViewModel
                    {
                        DistritoViewModel = distritoViewModel,
                        DisciplinaViewModel = disciplinaViewModel
                    });
                }
            }

            r102ViewModel.DistritosViewModel = distritosViewModel;
            r102ViewModel.DisciplinasViewModel = disciplinasViewModel;

            return View(r102ViewModel);
        }
    }
}