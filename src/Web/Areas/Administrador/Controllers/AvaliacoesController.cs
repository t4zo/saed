using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Web.Areas.Administrador.Controllers
{
    [Authorize(AuthorizationConstants.Permissions.Avaliacoes.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class AvaliacoesController : Controller
    {
        private readonly IAsyncRepository<Avaliacao> _avaliacaoRepository;
        private readonly IUnityOfWork _uow;

        public AvaliacoesController(IAsyncRepository<Avaliacao> avaliacaoRepository, IUnityOfWork uow)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _avaliacaoRepository.ListAllAsync());
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
                await _uow.CommitAsync();
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
                    await _uow.CommitAsync();
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

            await _uow.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
