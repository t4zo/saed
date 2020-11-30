using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAED.Core.Constants;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Aplicador.ViewModels;
using SAED.Web.Extensions;

namespace SAED.Web.Areas.Aplicador.Controllers
{
    [Area(AuthorizationConstants.Areas.Aplicador)]
    public class SelecaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelecaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Selecao.View)]
        public IActionResult Index()
        {
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Selecao.View)]
        [HttpPost]
        public async Task<IActionResult> Index(ChooseAlunoRequest chooseAlunoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            chooseAlunoRequest.Escola = await _context.Escolas.FindAsync(chooseAlunoRequest.EscolaId);
            chooseAlunoRequest.Etapa = await _context.Etapas.FindAsync(chooseAlunoRequest.EtapaId);
            chooseAlunoRequest.Turma = await _context.Turmas.FindAsync(chooseAlunoRequest.TurmaId);
            chooseAlunoRequest.Aluno = await _context.Alunos.FindAsync(chooseAlunoRequest.AlunoId);

            chooseAlunoRequest.Aluno.Turma = null;
            chooseAlunoRequest.Etapa.Turmas = null;
            chooseAlunoRequest.Turma.Alunos = null;
            chooseAlunoRequest.Turma.Etapa = null;
            
            HttpContext.Session.Set("alunoMetadata", chooseAlunoRequest);
            
            return RedirectToAction(nameof(Index), "Dashboard");
        }
    }
}