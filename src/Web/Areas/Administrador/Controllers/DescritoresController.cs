using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Specifications;
using SAED.Infrastructure.Data;
using System.Linq;
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
        private readonly ApplicationDbContext _context;

        public DescritoresController(
            IAsyncRepository<Disciplina> disciplinasRepository,
            IAsyncRepository<Tema> temasRepository,
            IAsyncRepository<Descritor> descritoresRepository,
            ApplicationDbContext context
            )
        {
            _disciplinasRepository = disciplinasRepository;
            _temasRepository = temasRepository;
            _descritoresRepository = descritoresRepository;
            _context = context;
        }

        public async Task<IActionResult> Index(int? disciplinaId, int? temaId)
        {
            var disciplinas = await _disciplinasRepository.ListAllAsync();
            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome", disciplinaId);

            var descritoresSpecification = new DescritoresWithSpecification();
            var descritores = await _descritoresRepository.ListAsync(descritoresSpecification);

            if (disciplinaId.HasValue)
            {
                var specification = new TemasWithSpecification(t => t.DisciplinaId == disciplinaId.Value);

                var temas = await _temasRepository.ListAsync(specification);
                ViewBag.Temas = new SelectList(temas, "Id", "Nome", temaId);

                descritores = descritores.Where(d => d.Tema.DisciplinaId == disciplinaId.Value).ToList();

                if (temaId.HasValue)
                {
                    descritores = descritores.Where(d => d.TemaId == temaId.Value).ToList();
                }
            }

            return View(descritores);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            var specification = new TemasWithSpecification();
            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.ToListAsync(), "Id", "Nome");
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(specification), "Id", "Nome");

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
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var specification = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(specification), "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var descritor = await _context.Descritores.AsNoTracking().Include("Tema.Disciplina").FirstOrDefaultAsync(x => x.Id == id);

            if (descritor is null)
            {
                return NotFound();
            }

            var specification = new TemasWithSpecification();
            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.ToListAsync(), "Id", "Nome", descritor.Tema.DisciplinaId);
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(specification), "Id", "Nome", descritor.TemaId);

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
                    await _context.SaveChangesAsync();
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

            var specification = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(specification), "Id", "Nome", descritor.TemaId);

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

            var specification = new TemasWithSpecification();
            ViewData["TemaId"] = new SelectList(await _temasRepository.ListAsync(specification), "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descritor = await _descritoresRepository.GetByIdAsync(id);
            await _descritoresRepository.DeleteAsync(descritor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
