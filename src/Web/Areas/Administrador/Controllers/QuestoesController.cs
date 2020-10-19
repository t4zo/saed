using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class QuestoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuestoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.View)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questoes.Include(x => x.Descritor).OrderBy(x => x.Descritor.Nome).ToListAsync());
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["DescritorId"] = new SelectList(await _context.Descritores.ToListAsync(), "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Questao questao)
        {
            if (ModelState.IsValid)
            {
                await _context.Questoes.AddAsync(questao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DescritorId"] = new SelectList(await _context.Descritores.ToListAsync(), "Id", "Nome", questao.DescritorId);

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var questao = await _context.Questoes.FindAsync(id);

            if (questao is null)
            {
                return NotFound();
            }

            ViewData["DescritorId"] = new SelectList(await _context.Descritores.ToListAsync(), "Id", "Nome", questao.DescritorId);

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Questao questao)
        {
            if (id != questao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var entity = await _context.Questoes.FindAsync(id);
                    if (entity is null)
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

            ViewData["DescritorId"] = new SelectList(await _context.Descritores.ToListAsync(), "Id", "Nome", questao.DescritorId);

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var questao = await _context.Questoes.FindAsync(id);

            if (questao is null)
            {
                return NotFound();
            }

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questao = await _context.Questoes.FindAsync(id);
            _context.Remove(questao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
