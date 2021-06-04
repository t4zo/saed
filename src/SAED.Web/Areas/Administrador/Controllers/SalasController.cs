using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Persistence.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class SalasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? escolaId)
        {
            var salas = await _context.Salas.Include(s => s.Escola).ToListAsync();

            if (escolaId.HasValue)
            {
                salas = salas.Where(x => x.EscolaId == escolaId).ToList();
            }

            ViewBag.EscolaId = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome");

            return View(salas);
        }

        public IActionResult Create()
        {
            ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", sala.EscolaId);

            return View(sala);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sala = await _context.Salas.FindAsync(id);

            if (sala is null)
            {
                return NotFound();
            }

            ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", sala.EscolaId);

            return View(sala);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sala sala)
        {
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Salas.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", sala.EscolaId);

            return View(sala);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sala = await _context.Salas.Include(s => s.Escola)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sala is null)
            {
                return NotFound();
            }

            return View(sala);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            _context.Salas.Remove(sala);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}