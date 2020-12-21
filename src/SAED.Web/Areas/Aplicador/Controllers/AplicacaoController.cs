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

            var questoesRespondidas = HttpContext.Session.Get<List<int>>(SessionConstants.QuestoesRespondidas);
            if (questoesRespondidas is null)
            {
                questoesRespondidas = new List<int> {respostaQuestaoViewModel.QuestaoId};
            }
            else
            {
                questoesRespondidas.Add(respostaQuestaoViewModel.QuestaoId);
            }

            var respostasAluno = HttpContext.Session.Get<RespostasAlunoViewModel>(SessionConstants.RespostasAluno);
            if (respostasAluno is null)
            {
                respostasAluno = new RespostasAlunoViewModel
                {
                    AvaliacaoId = respostaQuestaoViewModel.AvaliacaoId,
                    AlunoId = respostaQuestaoViewModel.AlunoId,
                    AlternativasId = new List<int> {respostaQuestaoViewModel.AlternativaId}
                };
            }
            else
            {
                respostasAluno.AlternativasId.Add(respostaQuestaoViewModel.AlternativaId);
            }

            var questoesPendentes = questoes.Where(x => questoesRespondidas.Any(questaoId => x.Id != questaoId)).Select(x => x.Id).ToList();

            HttpContext.Session.Set(SessionConstants.QuestoesPendentes, questoesPendentes);
            HttpContext.Session.Set(SessionConstants.QuestoesRespondidas, questoesRespondidas);
            HttpContext.Session.Set(SessionConstants.RespostasAluno, respostasAluno);

            return RedirectToAction(nameof(Index), new {questaoId = questoesPendentes.First()});
        }
    }
}