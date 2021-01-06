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
    public class TurmasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TurmasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var turmas = _context.Turmas
                .AsNoTracking()
                .Include(t => t.Etapa)
                .Include(t => t.Forma)
                .Include(t => t.Sala)
                .Include(t => t.Turno);

            return View(await turmas.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome");
            ViewData["FormaId"] = new SelectList(_context.Formas, "Id", "Nome");
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome");
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Turma turma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turma);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome", turma.EtapaId);
            ViewData["FormaId"] = new SelectList(_context.Formas, "Id", "Nome", turma.FormaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", turma.SalaId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Nome", turma.TurnoId);

            return View(turma);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var turma = await _context.Turmas.AsNoTracking().Include(x => x.Sala).FirstOrDefaultAsync(x => x.Id == id);
            var salasEscola = await _context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).ToListAsync();

            if (turma is null)
            {
                return NotFound();
            }

            ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome", salasEscola.Select(x => x.EscolaId).First());

            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome", turma.EtapaId);
            ViewData["FormaId"] = new SelectList(_context.Formas, "Id", "Nome", turma.FormaId);
            ViewData["SalaId"] = new SelectList(salasEscola, "Id", "Nome", turma.SalaId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Nome", turma.TurnoId);

            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Turma turma)
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Turmas.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome", turma.EtapaId);
            ViewData["FormaId"] = new SelectList(_context.Formas, "Id", "Nome", turma.FormaId);
            ViewData["SalaId"] = new SelectList(_context.Salas, "Id", "Nome", turma.SalaId);
            ViewData["TurnoId"] = new SelectList(_context.Turnos, "Id", "Nome", turma.TurnoId);

            return View(turma);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var turma = await _context.Turmas
                .Include(t => t.Etapa)
                .Include(t => t.Forma)
                .Include(t => t.Sala)
                .Include(t => t.Turno)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (turma is null)
            {
                return NotFound();
            }

            return View(turma);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            _context.Turmas.Remove(turma);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}