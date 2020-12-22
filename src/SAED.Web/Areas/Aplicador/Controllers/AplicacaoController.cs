using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Areas.Aplicador.ViewModels;
using SAED.Web.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SAED.Web.Areas.Aplicador.Controllers
{
    [Area(AuthorizationConstants.Areas.Aplicador)]
    [Route("[controller]")]
    public class AplicacaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AplicacaoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{questaoId}")]
        public IActionResult Index(int questaoId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);
            var questoes = HttpContext.Session.Get<List<Questao>>(SessionConstants.Questoes);
            var aluno = HttpContext.Session.Get<DashboardAplicadorViewModel>(SessionConstants.Aluno).Aluno;

            if (questoes is null)
            {
                return BadRequest();
            }

            var questao = questoes.FirstOrDefault(x => x.Id == questaoId);

            if (questao is null)
            {
                return BadRequest();
            }

            questao.Descritor.Questoes = null;
            questao.Descritor.Tema.Descritores = null;
            questao.Descritor.Tema.Disciplina.Temas = null;

            var respostaQuestaoViewModel = _mapper.Map<RespostaQuestaoViewModel>(questao);
            respostaQuestaoViewModel.AvaliacaoId = avaliacao.Id;
            respostaQuestaoViewModel.AlunoId = aluno.Id;
            respostaQuestaoViewModel.Questao = questao;

            return View(respostaQuestaoViewModel);
        }

        [HttpPost]
        public IActionResult Proximo(RespostaQuestaoViewModel respostaQuestaoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var questoes = HttpContext.Session.Get<List<Questao>>(SessionConstants.Questoes);
            var questao = questoes.First(x => x.Id == respostaQuestaoViewModel.QuestaoId);
            var disciplina = questao.Descritor.Tema.Disciplina;
            
            var questaoViewModel = _mapper.Map<QuestaoViewModel>(questao);
            questaoViewModel.AlternativaEscolhida = questao.Alternativas.First(x => x.Id == respostaQuestaoViewModel.AlternativaId);

            var questoesPendentes = HttpContext.Session.Get<List<Questao>>(SessionConstants.QuestoesPendentes);
            var questoesPendentesDisciplina = questoes
                .Where(x => x.Descritor.Tema.DisciplinaId == disciplina.Id)
                .ToList();
            var ultimaQuestaoDisciplina = questoesPendentesDisciplina.Last().Id == respostaQuestaoViewModel.QuestaoId;

            if (questoesPendentes is null)
            {
                questoesPendentes = questoesPendentesDisciplina.Where(x => x.Id != questao.Id).ToList();
            }
            else
            {
                if (questoesPendentes.Any(x => x.Id == questao.Id))
                {
                    questoesPendentes.Remove(questao);
                }
            }

            var respostasAluno = HttpContext.Session.Get<RespostasAlunoViewModel>(SessionConstants.RespostasAluno);
            
            if (respostasAluno is null)
            {
                respostasAluno = new RespostasAlunoViewModel
                {
                    AvaliacaoId = respostaQuestaoViewModel.AvaliacaoId,
                    AlunoId = respostaQuestaoViewModel.AlunoId,
                    Questoes = new List<QuestaoViewModel> { questaoViewModel }
                };
            }
            else
            {
                if (respostasAluno.Questoes.Any(x => x.Id == questaoViewModel.Id))
                {
                    respostasAluno.Questoes.Remove(questaoViewModel);
                }
                
                respostasAluno.Questoes.Add(questaoViewModel);
            }


            HttpContext.Session.Set(SessionConstants.QuestoesPendentes, questoesPendentes);
            HttpContext.Session.Set(SessionConstants.RespostasAluno, respostasAluno);

            ViewBag.UltimaQuestaoDisciplina = ultimaQuestaoDisciplina;
            
            if (ultimaQuestaoDisciplina)
            {
                return RedirectToAction(nameof(Index), "Dashboard");
            }
            
            return RedirectToAction(nameof(Index), new {questaoId = questoesPendentes.First().Id});
        }
    }
}