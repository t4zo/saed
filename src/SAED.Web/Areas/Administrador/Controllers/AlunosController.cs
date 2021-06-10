using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Persistence.Data;
using SAED.Persistence.Identity;
using SAED.Web.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserService _userService;

        public AlunosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, UserService userService)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> Index(int? escolaId, int? etapaId)
        {
            var alunos = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Turma)
                .ThenInclude(x => x.Etapa)
                .Include(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .ToListAsync();

            if (escolaId.HasValue)
            {
                alunos = alunos.Where(x => x.Turma.Sala.EscolaId == escolaId).ToList();
            }

            if (etapaId.HasValue)
            {
                alunos = alunos.Where(x => x.Turma.EtapaId == etapaId).ToList();
            }

            ViewBag.Escolas = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", escolaId);
            ViewBag.Etapas = new SelectList(_context.Etapas.OrderBy(x => x.Nome), "Id", "Nome", etapaId);

            return View(alunos);
        }

        public IActionResult Create()
        {
            ViewBag.EscolaId = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            if (aluno.TurmaId == 0)
            {
                ModelState.AddModelError("", "Escola e/ou Sala e/ou Turma inválido(s)");

                ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome");

                return View(aluno);
            }

            var turma = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == aluno.TurmaId);

            if (aluno.Cpf.Codigo == null || !aluno.Cpf.IsValid())
            {
                ModelState.AddModelError("", "CPF Inválido");
                
                ResetDataSelectValues(turma);
                
                return View(aluno);
            }

            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey("Nascimento"))
                {
                    ModelState.AddModelError("", "Data de Nascimento Inválida");
                }
                else
                {
                    ModelState.AddModelError("", "Algum campo está inválido");
                }

                ResetDataSelectValues(turma);

                return View(aluno);
            }

            var result = await _userService.CreateUserAsync(aluno.Cpf.Normalize(), aluno.Cpf.Normalize(), aluno.Cpf.Normalize());

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email e/ou Senha Inválida(s)/Existente");
                
                ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
                ViewData["SalaId"] = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome", turma.SalaId);
                ViewData["TurmaId"] = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);
                return View(aluno);
            }

            _context.Add(aluno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Cpf)
                .Include(a => a.Turma)
                .ThenInclude(x => x.Etapa)
                .Include(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == id);

            var salasEscola = await _context.Salas
                .AsNoTracking()
                .Where(x => x.EscolaId == aluno.Turma.Sala.EscolaId)
                .ToListAsync();

            var turmasSala = await _context.Turmas
                .AsNoTracking()
                .Where(x => x.SalaId == aluno.Turma.SalaId)
                .ToListAsync();

            if (aluno is null)
            {
                return NotFound();
            }

            ViewBag.EscolaId = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", aluno.Turma.Sala.EscolaId);
            ViewBag.SalaId = new SelectList(salasEscola, "Id", "Nome", aluno.Turma.SalaId);
            ViewBag.TurmaId = new SelectList(turmasSala, "Id", "Nome", aluno.TurmaId);

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (aluno.TurmaId == 0)
            {
                ModelState.AddModelError("", "Escola e/ou Sala e/ou Turma inválido(s)");
                
                ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome");

                return View(aluno);
            }

            var turma = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == aluno.TurmaId);

            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey("Nascimento"))
                {
                    ModelState.AddModelError("", "Data de Nascimento Inválida");
                }
                else
                {
                    ModelState.AddModelError("", "Algum campo está inválido");
                }

                ResetDataSelectValues(turma);

                return View(aluno);
            }

            if (aluno.Cpf.Codigo == null || !aluno.Cpf.IsValid())
            {
                ModelState.AddModelError("", "CPF Inválido");
                
                ResetDataSelectValues(turma);

                return View(aluno);
            }

            try
            {
                var alunoOriginal = await _context.Alunos.FindAsync(aluno.Id);

                if (aluno.Cpf.Normalize() != alunoOriginal.Cpf.Normalize())
                {
                    var result = await _userService.UpdateUserAsync(aluno, alunoOriginal);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Email e/ou Senha Inválida(s)/Existente");
                        
                        ResetDataSelectValues(turma);

                        return View(aluno);
                    }
                }
                
                _context.Update(aluno);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alunos.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Cpf)
                .Include(a => a.Turma)
                .ThenInclude(x => x.Etapa)
                .Include(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (aluno is null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            _context.Alunos.Remove(aluno);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == aluno.Cpf.Codigo);
            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private void ResetDataSelectValues(Turma turma)
        {
            ViewBag.EscolaId = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
            ViewBag.SalaId = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome",turma.SalaId);
            ViewBag.TurmaId = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);
        }
    }
}