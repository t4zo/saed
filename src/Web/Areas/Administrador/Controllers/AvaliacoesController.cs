using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Authorize(AuthorizationConstants.Permissions.Avaliacoes.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class AvaliacoesController : Controller
    {
        private readonly IAsyncRepository<Avaliacao> _avaliacaoRepository;
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(IAsyncRepository<Avaliacao> avaliacaoRepository, ApplicationDbContext context)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _avaliacaoRepository.ListAllAsync();
            return View(avaliacoes.OrderBy(x => x.Codigo));
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
            if (ModelState.IsValid)
            {
                await _avaliacaoRepository.AddAsync(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);

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

            if (ModelState.IsValid)
            {
                try
                {
                    await _avaliacaoRepository.UpdateAsync(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var entity = _avaliacaoRepository.GetByIdAsync(avaliacao.Id);
                    if (entity.Id != id)
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

            return View(avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);

            if (avaliacao is null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);
            await _avaliacaoRepository.DeleteAsync(avaliacao);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
