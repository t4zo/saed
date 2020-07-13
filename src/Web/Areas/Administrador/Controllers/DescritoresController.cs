using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class DescritoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnityOfWork _uow;

        public DescritoresController(ApplicationDbContext context, IUnityOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Descritores.Include(d => d.Tema);
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["TemaId"] = new SelectList(_context.Temas, "Id", "Nome");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Descritor descritor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(descritor);
                await _uow.CommitAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["TemaId"] = new SelectList(_context.Temas, "Id", "Nome", descritor.TemaId);
        
            return View(descritor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var descritor = await _context.Descritores.FindAsync(id);

            if (descritor is null)
            {
                return NotFound();
            }

            ViewData["TemaId"] = new SelectList(_context.Temas, "Id", "Nome", descritor.TemaId);
            
            return View(descritor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Descritor descritor)
        {
            if (id != descritor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(descritor);
                    await _uow.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Descritores.Any(e => e.Id == id))
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

            ViewData["TemaId"] = new SelectList(_context.Temas, "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var descritor = await _context.Descritores.Include(d => d.Tema)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (descritor is null)
            {
                return NotFound();
            }

            return View(descritor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descritor = await _context.Descritores.FindAsync(id);
            _context.Descritores.Remove(descritor);
            await _uow.CommitAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
