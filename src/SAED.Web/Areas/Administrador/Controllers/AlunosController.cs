using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? escolaId, int? etapaId)
        {
            var alunos = await _context.Alunos
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
            ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome");
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
            }

            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Nome", aluno.TurmaId);

            return View(aluno);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
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
            ViewData["TurmaId"] = new SelectList(turmasSala, "Id", "Nome", aluno.TurmaId);

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

            if (ModelState.IsValid)
            {
                try
                {
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

            ViewData["TurmaId"] = new SelectList(_context.Turmas, "Id", "Nome", aluno.TurmaId);

            return View(aluno);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
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

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}