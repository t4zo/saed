using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Aplicador.Controllers
{
    [Area(AuthorizationConstants.Areas.Aplicador)]
    [Route("[Controller]")]
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
            var questoes = HttpContext.Session.Get<List<Questao>>(SessionConstants.Questoes);
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

            return View(_mapper.Map<QuestaoViewModel>(questao));
        }
    }
}
