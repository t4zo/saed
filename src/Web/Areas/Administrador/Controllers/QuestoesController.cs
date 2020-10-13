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
    [Authorize(AuthorizationConstants.Permissions.Questoes.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class QuestoesController : Controller
    {
        private readonly IAsyncRepository<Descritor> _descritoresRepository;
        private readonly IAsyncRepository<Questao> _questoesRepository;
        private readonly ApplicationDbContext _context;

        public QuestoesController(
            IAsyncRepository<Descritor> descritoresRepository,
            IAsyncRepository<Questao> questoesRepository,
            ApplicationDbContext context
        )
        {
            _descritoresRepository = descritoresRepository;
            _questoesRepository = questoesRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var specification = new QuestoesWithSpecification();
            var questoes = await _questoesRepository.ListAsync(specification);
            return View(questoes.OrderBy(x => x.Descritor.Nome));
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["DescritorId"] = new SelectList(await _descritoresRepository.ListAllAsync(), "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Questao questao)
        {
            if (ModelState.IsValid)
            {
                await _questoesRepository.AddAsync(questao);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DescritorId"] = new SelectList(await _descritoresRepository.ListAllAsync(), "Id", "Nome", questao.DescritorId);

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var questao = await _questoesRepository.GetByIdAsync(id);

            if (questao is null)
            {
                return NotFound();
            }

            ViewData["DescritorId"] = new SelectList(await _descritoresRepository.ListAllAsync(), "Id", "Nome", questao.DescritorId);

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
                    await _questoesRepository.UpdateAsync(questao);
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

            ViewData["DescritorId"] = new SelectList(await _descritoresRepository.ListAllAsync(), "Id", "Nome", questao.DescritorId);

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var questao = await _questoesRepository.FirstOrDefaultAsync(new QuestoesWithSpecification(x => x.Id == id));

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
            var questao = await _questoesRepository.GetByIdAsync(id);
            await _questoesRepository.DeleteAsync(questao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
