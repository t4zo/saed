using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class EscolasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EscolasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.View)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Escolas.Include(x => x.Distrito).ToListAsync());
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Create)]
        public async Task<IActionResult> Create()
        {
            List<Distrito> distritos = await _context.Distritos.AsNoTracking().Include(x => x.Escolas).ToListAsync();

            ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome");
            ViewData["MatrizId"] = new SelectList(distritos.SelectMany(x => x.Escolas), "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escola escola)
        {
            List<Distrito> distritos = await _context.Distritos.Include(x => x.Escolas).ToListAsync();

            if (!ModelState.IsValid)
            {
                ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome", escola.DistritoId);
                ViewData["MatrizId"] =
                    new SelectList(distritos.SelectMany(x => x.Escolas), "Id", "Email", escola.MatrizId);

                return View(escola);
            }

            await _context.AddAsync(escola);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            List<Distrito> distritos = await _context.Distritos.AsNoTracking().Include(x => x.Escolas).ToListAsync();
            Escola escola = distritos.FirstOrDefault(x => x.Escolas.Any(y => y.Id == id))?.Escolas.First();

            if (escola is null)
            {
                return NotFound();
            }

            ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(distritos.SelectMany(x => x.Escolas), "Id", "Nome", escola.MatrizId);

            return View(escola);
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Escola escola)
        {
            if (id != escola.Id)
            {
                return NotFound();
            }

            List<Distrito> distritos = await _context.Distritos.Include(x => x.Escolas).ToListAsync();

            if (!ModelState.IsValid)
            {
                ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome", escola.DistritoId);
                ViewData["MatrizId"] =
                    new SelectList(distritos.SelectMany(x => x.Escolas), "Id", "Email", escola.MatrizId);

                return View(escola);
            }

            try
            {
                _context.Update(escola);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                Escola entity = await _context.Escolas.FindAsync(id);
                if (entity is null)
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            Escola escola = await _context.Escolas.FindAsync(id);

            if (escola is null)
            {
                return NotFound();
            }

            return View(escola);
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Escola escola = await _context.Escolas.FindAsync(id);

            if (escola is null)
            {
                return NotFound();
            }

            _context.Remove(escola);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}