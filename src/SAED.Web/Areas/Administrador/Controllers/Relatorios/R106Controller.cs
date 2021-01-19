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
        public async Task<IActionResult> R106()
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
            var alunos = respostas.Select(x => x.Aluno).Distinct().ToList();

            var qtdTotalQuestoes = respostas
                .Select(x => x.Alternativa.Questao)
                .Distinct()
                .Count();

            var r106ViewModel = new R106ViewModel
            {
                QtdTotalQuestoes = qtdTotalQuestoes
            };

            var alunosViewModel = new List<AlunoViewModel>();
            var disciplinasViewModel = new List<DisciplinaViewModel>();
            foreach (var escola in escolas)
            {
                foreach (var etapa in etapas)
                {
                    foreach (var aluno in alunos)
                    {
                        foreach (var disciplina in disciplinas)
                        {
                            var qtdQuestoesDisciplina = respostas
                                .Where(x => x.Aluno.Turma.EtapaId == etapa.Id)
                                .Where(x => x.Alternativa.Questao.Descritor.Tema.DisciplinaId == disciplina.Id)
                                .Select(x => x.Alternativa.Questao)
                                .Distinct()
                                .Count();

                            var qtdRespostasCorretas = respostas
                                .Where(x => x.AlunoId == aluno.Id)
                                .Where(x => x.Alternativa.Questao.Descritor.Tema.DisciplinaId == disciplina.Id)
                                .Select(x => x.Alternativa)
                                .Count(x => x.Correta);

                            var alunoViewModel = new AlunoViewModel
                            {
                                Escola = escola,
                                Etapa = etapa,
                                Aluno = aluno,
                                Disciplina = disciplina
                            };
                            alunosViewModel.Add(alunoViewModel);

                            var disciplinaViewModel = new DisciplinaViewModel
                            {
                                Disciplina = disciplina,
                                Escola = escola,
                                Etapa = etapa,
                                Aluno = aluno,
                                QtdQuestoesDisciplina = qtdQuestoesDisciplina,
                                QtdRespostasCorretas = qtdRespostasCorretas
                            };
                            disciplinasViewModel.Add(disciplinaViewModel);

                            r106ViewModel.ResultadoAlunosViewModel.Add(new ResultadoAlunoViewModel
                            {
                                AlunoViewModel = alunoViewModel,
                                DisciplinaViewModel = disciplinaViewModel,
                                QtdRespostasCorretas = disciplinaViewModel.QtdRespostasCorretas,
                                QtdQuestoes = disciplinaViewModel.QtdQuestoesDisciplina,
                            });
                        }
                    }
                }
            }

            r106ViewModel.AlunosViewModel = alunosViewModel;
            r106ViewModel.DisciplinasViewModel = disciplinasViewModel;

            return View(r106ViewModel);
        }
    }
}