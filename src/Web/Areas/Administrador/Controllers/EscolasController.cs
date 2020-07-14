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
    [Authorize(AuthorizationConstants.Permissions.Escolas.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class EscolasController : Controller
    {
        private readonly IAsyncRepository<Escola> _escolasRepository;
        private readonly IAsyncRepository<Distrito> _distritosRepository;
        private readonly IUnityOfWork _uow;

        public EscolasController(IAsyncRepository<Escola> escolasRepository, IAsyncRepository<Distrito> distritosRepository, IUnityOfWork uow)
        {
            _escolasRepository = escolasRepository;
            _distritosRepository = distritosRepository;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            var specification = new EscolasWithSpecification();
            var escolas = await _escolasRepository.ListAsync(specification);
            return View(escolas.OrderBy(x => x.Nome));
        }

        public async Task<IActionResult> Create()
        {
            var specification = new EscolasWithSpecification();
            var escolas = await _escolasRepository.ListAsync(specification);
            var distritos = escolas.Select(x => x.Distrito).ToHashSet().OrderBy(x => x.Id);

            ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome");
            ViewData["MatrizId"] = new SelectList(escolas, "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escola escola)
        {
            var specification = new EscolasWithSpecification();
            var escolas = await _escolasRepository.ListAsync(specification);
            var distritos = escolas.Select(x => x.Distrito).ToHashSet().OrderBy(x => x.Id);

            if (ModelState.IsValid)
            {
                await _escolasRepository.AddAsync(escola);
                await _uow.CommitAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(escolas, "Id", "Email", escola.MatrizId);

            return View(escola);
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var escola = await _escolasRepository.GetByIdAsync(id);

            if (escola is null)
            {
                return NotFound();
            }

            ViewData["DistritoId"] = new SelectList(await _distritosRepository.ListAllAsync(), "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(await _escolasRepository.ListAllAsync(), "Id", "Nome", escola.MatrizId);

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

            if (ModelState.IsValid)
            {
                try
                {
                    await _escolasRepository.UpdateAsync(escola);
                    await _uow.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var entity = await _escolasRepository.GetByIdAsync(id);
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

            ViewData["DistritoId"] = new SelectList(await _distritosRepository.ListAllAsync(), "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(await _escolasRepository.ListAllAsync(), "Id", "Email", escola.MatrizId);

            return View(escola);
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var escola = await _escolasRepository.FirstOrDefaultAsync(new EscolasWithSpecification(x => x.Id == id));

            if (escola is null)
            {
                return NotFound();
            }

            ViewData["DistritoId"] = escola.Distrito;

            return View(escola);
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escola = await _escolasRepository.GetByIdAsync(id);
            await _escolasRepository.DeleteAsync(escola);
            await _uow.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
