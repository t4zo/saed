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
    public class AvaliacoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.View)]
        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _context.Avaliacoes.AsNoTracking().ToListAsync();
            return View(avaliacoes);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Avaliacao avaliacao)
        {
            if (!ModelState.IsValid)
            {
                return View(avaliacao);
            }

            await _context.AddAsync(avaliacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao is null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(avaliacao);
            }

            try
            {
                _context.Update(avaliacao);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var entity = await _context.Avaliacoes.FindAsync(avaliacao.Id);
                if (entity is null)
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao is null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao is null)
            {
                return NotFound();
            }

            _context.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}