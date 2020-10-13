using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class DistritosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DistritosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var distritos = await _context.Distritos.ToListAsync();
            return View(distritos.OrderBy(x => x.Nome));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Distrito distrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(distrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(distrito);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var distrito = await _context.Distritos.FindAsync(id);

            if (distrito is null)
            {
                return NotFound();
            }

            return View(distrito);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Distrito distrito)
        {
            if (id != distrito.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(distrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Distritos.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(distrito);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var distrito = await _context.Distritos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (distrito is null)
            {
                return NotFound();
            }

            return View(distrito);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var distrito = await _context.Distritos.FindAsync(id);
            _context.Distritos.Remove(distrito);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
