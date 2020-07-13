using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Specifications;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Authorize(AuthorizationConstants.Permissions.Temas.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class TemasController : Controller
    {
        private readonly IAsyncRepository<Disciplina> _disciplinasRepository;
        private readonly IAsyncRepository<Tema> _temasRepository;
        private readonly IUnityOfWork _uow;

        public TemasController(IAsyncRepository<Disciplina> disciplinasRepository, IAsyncRepository<Tema> temasRepository, IUnityOfWork uow)
        {
            _disciplinasRepository = disciplinasRepository;
            _temasRepository = temasRepository;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var specification = new TemasWithSpecification();
            return View(await _temasRepository.ListAsync(specification));
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            var specification = new TemasWithSpecification();
            var temas = await _temasRepository.ListAsync(specification);
            var disciplinas = temas.Select(x => x.Disciplina).ToHashSet().OrderBy(x => x.Id);

            ViewData["DisciplinaId"] = new SelectList(disciplinas, "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tema tema)
        {
            var specification = new TemasWithSpecification();
            var temas = await _temasRepository.ListAsync(specification);
            var disciplinas = await _disciplinasRepository.ListAllAsync();

            if (ModelState.IsValid)
            {
                await _temasRepository.AddAsync(tema);
                await _uow.CommitAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DisciplinaId"] = new SelectList(disciplinas, "Id", "Nome", tema.DisciplinaId);

            return View(tema);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var tema = await _temasRepository.GetByIdAsync(id);

            if (tema is null)
            {
                return NotFound();
            }

            ViewData["DisciplinaId"] = new SelectList(await _disciplinasRepository.ListAllAsync(), "Id", "Nome", tema.DisciplinaId);

            return View(tema);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tema tema)
        {
            if (id != tema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _temasRepository.UpdateAsync(tema);
                    await _uow.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var entity = await _temasRepository.GetByIdAsync(id);
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

            ViewData["DisciplinaId"] = new SelectList(await _disciplinasRepository.ListAllAsync(), "Id", "Nome", tema.DisciplinaId);

            return View(tema);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var tema = await _temasRepository.FirstAsync(new TemasWithSpecification(x => x.Id == id));

            if (tema is null)
            {
                return NotFound();
            }

            ViewData["DisciplinaId"] = tema.Disciplina;

            return View(tema);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tema = await _temasRepository.GetByIdAsync(id);
            await _temasRepository.DeleteAsync(tema);
            await _uow.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
