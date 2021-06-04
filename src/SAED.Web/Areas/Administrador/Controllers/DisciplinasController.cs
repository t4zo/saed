using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Persistence.Data;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DisciplinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.View)]
        public async Task<IActionResult> Index()
        {
            var disciplinas = await _context.Disciplinas.AsNoTracking().ToListAsync();
            return View(disciplinas);
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
            if (!ModelState.IsValid)
            {
                return View(disciplina);
            }

            await _context.AddAsync(disciplina);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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

            if (!ModelState.IsValid)
            {
                return View(disciplina);
            }

            try
            {
                _context.Update(disciplina);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var entity = await _context.Disciplinas.FindAsync(id);
                if (entity is null)
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // [Authorize(AuthorizationConstants.Permissions.Disciplinas.Delete)]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var disciplina = await _context.Disciplinas.FindAsync(id);
        //
        //     if (disciplina is null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(disciplina);
        // }

        [Authorize(AuthorizationConstants.Permissions.Disciplinas.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina is null)
            {
                return NotFound();
            }

            _context.Remove(disciplina);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}