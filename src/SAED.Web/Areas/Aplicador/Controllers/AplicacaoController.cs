using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
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
        private readonly IMapper _mapper;

        public AplicacaoController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{disciplinaId}/{verificacao}")]
        public IActionResult Index(int disciplinaId, string verificacao)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);
            var questoes = HttpContext.Session.Get<List<Questao>>(SessionConstants.Questoes);
            var questoesDisciplina = questoes.Where(x => x.Descritor.Tema.DisciplinaId == disciplinaId);
            var questao = questoesDisciplina.First();
            var aluno = HttpContext.Session.Get<DashboardAplicadorViewModel>(SessionConstants.Aluno).Aluno;

            HttpContext.Session.Remove(SessionConstants.QuestoesPendentesDisciplina);

            var respostaQuestaoViewModel = _mapper.Map<RespostaViewModel>(questao);
            respostaQuestaoViewModel.Populate(avaliacao.Id, aluno.Id, questao);

            return View(respostaQuestaoViewModel);
        }

        [HttpGet("{questaoId}")]
        public IActionResult Proximo(int questaoId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);
            var questoes = HttpContext.Session.Get<List<Questao>>(SessionConstants.Questoes);
            var questao = questoes.First(x => x.Id == questaoId);
            var aluno = HttpContext.Session.Get<DashboardAplicadorViewModel>(SessionConstants.Aluno).Aluno;

            var respostaQuestaoViewModel = _mapper.Map<RespostaViewModel>(questao);
            respostaQuestaoViewModel.Populate(avaliacao.Id, aluno.Id, questao);

            return View(nameof(Index), respostaQuestaoViewModel);
        }

        [HttpPost]
        public IActionResult Proximo(RespostaViewModel respostaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var questoes = HttpContext.Session.Get<List<Questao>>(SessionConstants.Questoes);
            var questao = questoes.First(x => x.Id == respostaViewModel.QuestaoId);
            var disciplina = questao.Descritor.Tema.Disciplina;

            var respostas = HttpContext.Session.Get<RespostasViewModel>(SessionConstants.RespostasAluno);
            var resposta = _mapper.Map<RespostaViewModel>(questao);
            resposta.AlternativaEscolhida = questao.Alternativas.First(x => x.Id == respostaViewModel.AlternativaEscolhidaId);

            var questoesPendentesDisciplina = HttpContext.Session.Get<List<Questao>>(SessionConstants.QuestoesPendentesDisciplina);

            if (questoesPendentesDisciplina is null)
            {
                questoesPendentesDisciplina = questoes.Where(x => x.Id != questao.Id && x.Descritor.Tema.DisciplinaId == disciplina.Id).ToList();
            }
            else
            {
                if (questoesPendentesDisciplina.Any(x => x.Id == questao.Id))
                {
                    questoesPendentesDisciplina.Remove(questao);
                }
            }

            if (respostas is null)
            {
                respostas = new RespostasViewModel
                {
                    AvaliacaoId = respostaViewModel.AvaliacaoId,
                    AlunoId = respostaViewModel.AlunoId,
                    Respostas = new List<RespostaViewModel> {resposta}
                };
            }
            else
            {
                var oldResposta = respostas.Respostas.FirstOrDefault(x => x.QuestaoId == resposta.QuestaoId);
                if (oldResposta is not null)
                {
                    respostas.Respostas.Remove(oldResposta);
                }

                respostas.Respostas.Add(resposta);
            }

            bool ultimaQuestaoDisciplina;
            if (questoesPendentesDisciplina.Count == 0)
            {
                ultimaQuestaoDisciplina = true;
            }
            else
            {
                ultimaQuestaoDisciplina = questoesPendentesDisciplina.Last().Id == respostaViewModel.QuestaoId;
            }

            HttpContext.Session.Set(SessionConstants.QuestoesPendentesDisciplina, questoesPendentesDisciplina);
            HttpContext.Session.Set(SessionConstants.RespostasAluno, respostas);

            if (ultimaQuestaoDisciplina)
            {
                return RedirectToAction(nameof(Index), "Dashboard");
            }

            return RedirectToAction(nameof(Proximo), new {questaoId = questoesPendentesDisciplina.First().Id});
        }
    }
}