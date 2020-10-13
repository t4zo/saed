using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Authorize(AuthorizationConstants.Permissions.Disciplinas.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DisciplinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var disciplinas = await _context.Disciplinas.ToListAsync();
            return View(disciplinas.OrderBy(x => x.Id));
        }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(disciplina);
        }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina is null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Disciplina disciplina)
        {
            if (id != disciplina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Disciplinas.Any(e => e.Id == id))
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

            return View(disciplina);
        }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var disciplina = await _context.Disciplinas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (disciplina is null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            _context.Disciplinas.Remove(disciplina);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
