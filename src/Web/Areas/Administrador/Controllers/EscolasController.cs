using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;

namespace Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class EscolasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnityOfWork _uow;

        public EscolasController(ApplicationDbContext context, IUnityOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Escolas.Include(e => e.Distrito).Include(e => e.Matriz);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["DistritoId"] = new SelectList(_context.Distritos, "Id", "Nome");
            ViewData["MatrizId"] = new SelectList(_context.Escolas, "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escola escola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escola);
                await _uow.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistritoId"] = new SelectList(_context.Distritos, "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(_context.Escolas, "Id", "Email", escola.MatrizId);

            return View(escola);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var escola = await _context.Escolas.FindAsync(id);

            if (escola is null)
            {
                return NotFound();
            }
            ViewData["DistritoId"] = new SelectList(_context.Distritos, "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(_context.Escolas, "Id", "Email", escola.MatrizId);

            return View(escola);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Escola escola)
        {
            if (id != escola.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escola);
                    await _uow.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Escolas.Any(e => e.Id == id))
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
            ViewData["DistritoId"] = new SelectList(_context.Distritos, "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(_context.Escolas, "Id", "Email", escola.MatrizId);

            return View(escola);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var escola = await _context.Escolas.Include(e => e.Distrito).Include(e => e.Matriz)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (escola is null)
            {
                return NotFound();
            }

            return View(escola);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escola = await _context.Escolas.FindAsync(id);
            _context.Escolas.Remove(escola);

            await _uow.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
