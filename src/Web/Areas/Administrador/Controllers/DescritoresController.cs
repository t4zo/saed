using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Specifications;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Authorize(AuthorizationConstants.Permissions.Descritores.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DescritoresController : Controller
    {
        private readonly IAsyncRepository<Disciplina> _disciplinasRepository;
        private readonly IAsyncRepository<Tema> _temasRepository;
        private readonly IAsyncRepository<Descritor> _descritoresRepository;
        private readonly IUnityOfWork _uow;

        public DescritoresController(
            IAsyncRepository<Disciplina> disciplinasRepository,
            IAsyncRepository<Tema> temasRepository,
            IAsyncRepository<Descritor> descritoresRepository,
            IUnityOfWork uow
            )
        {
            _disciplinasRepository = disciplinasRepository;
            _temasRepository = temasRepository;
            _descritoresRepository = descritoresRepository;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var descritores = new DescritoresWithSpecification();
            return View(await _descritoresRepository.ListAsync(descritores));
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            var temas = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(temas), "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Descritor descritor)
        {
            if (ModelState.IsValid)
            {
                await _descritoresRepository.AddAsync(descritor);
                await _uow.CommitAsync();

                return RedirectToAction(nameof(Index));
            }

            var temas = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(temas), "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var descritor = await _descritoresRepository.GetByIdAsync(id);

            if (descritor is null)
            {
                return NotFound();
            }

            var temas = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(temas), "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Update)]
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
                    await _descritoresRepository.UpdateAsync(descritor);
                    await _uow.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var entity = await _descritoresRepository.GetByIdAsync(id);
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

            var temas = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(temas), "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var descritor = await _descritoresRepository.GetByIdAsync(id);

            if (descritor is null)
            {
                return NotFound();
            }

            var temas = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(temas), "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descritor = await _descritoresRepository.GetByIdAsync(id);
            await _descritoresRepository.DeleteAsync(descritor);
            await _uow.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
