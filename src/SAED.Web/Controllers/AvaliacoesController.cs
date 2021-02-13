using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Extensions;
using SAED.Web.Extensions;
using System.Linq;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Controllers
{
    [Authorize]
    public class AvaliacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _context.Avaliacoes.AsNoTracking().ToListAsync();

            var userId = User.GetUserId();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var respostas = await _context.RespostaAlunos
                .AsNoTracking()
                .Include(x => x.Avaliacao)
                .Include(x => x.Aluno)
                .ThenInclude(x => x.Cpf)
                .ToListAsync();

            var containsResposta = respostas.Where(x => x.Aluno.Cpf.Normalize() == user.Email).ToList();
            if (containsResposta.Count != 0)
            {
                var avaliacoesAluno = containsResposta.Select(x => x.Avaliacao).Distinct().ToList();
                foreach (var avaliacaoAluno in avaliacoesAluno)
                {
                    avaliacaoAluno.RespostaAlunos = null;
                }

                avaliacoes = avaliacoes.Except(avaliacoesAluno).ToList();
            }

            ViewBag.Avaliacoes = new SelectList(avaliacoes, "Id", "Codigo");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            HttpContext.Session.Set(SessionConstants.Avaliacao, avaliacao);

            if (User.IsInRole(Roles.Superuser) || User.IsInRole(Roles.Administrador))
            {
                return Redirect($"{AuthorizationConstants.Areas.Administrador}/Dashboard".ToLower());
            }

            if (User.IsInRole(Roles.Aplicador))
            {
                return Redirect($"{AuthorizationConstants.Areas.Aplicador}/Selecao".ToLower());
            }

            if (User.IsInRole(Roles.Aluno))
            {
                var alunos = await _context.Alunos
                    .AsNoTracking()
                    .Include(x => x.Cpf)
                    .ToListAsync();

                var cpfAluno = User.Identity?.Name;

                var aluno = alunos.FirstOrDefault(x => x.Cpf.Normalize() == cpfAluno);
                return Redirect($"{AuthorizationConstants.Areas.Aplicador}/Selecao/{aluno?.Id}".ToLower());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}