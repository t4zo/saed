using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class AvaliacaoDisciplinasEtapasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoDisciplinasEtapasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());
            var avaliacaoDisciplinasEtapas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Avaliacao)
                .Include(x => x.Disciplina)
                .Include(x => x.Etapa)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .ToListAsync();

            return View(avaliacaoDisciplinasEtapas);
        }

        public IActionResult Create()
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");
            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AvaliacaoDisciplinaEtapa avaliacaoDisciplinaEtapa)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var avaliacaoDisciplinaEtapaExists = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .AnyAsync(
                    x => x.AvaliacaoId == avaliacao.Id &&
                         x.DisciplinaId == avaliacaoDisciplinaEtapa.DisciplinaId &&
                         x.EtapaId == avaliacaoDisciplinaEtapa.EtapaId
                );

            if (!avaliacaoDisciplinaEtapaExists)
            {
                avaliacaoDisciplinaEtapa.AvaliacaoId = avaliacao.Id;
                await _context.AddAsync(avaliacaoDisciplinaEtapa);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int disciplinaId, int etapaId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var avaliacaoDisciplinaEtapa = await _context.AvaliacaoDisciplinasEtapas
                .Include(x => x.Disciplina)
                .Include(x => x.Etapa)
                .FirstOrDefaultAsync(
                    x => x.AvaliacaoId == avaliacao.Id &&
                         x.DisciplinaId == disciplinaId &&
                         x.EtapaId == etapaId
                );

            if (avaliacaoDisciplinaEtapa is null)
            {
                return NotFound();
            }

            ViewData["AvaliacaoId"] =
                new SelectList(_context.Avaliacoes, "Id", "Codigo", avaliacaoDisciplinaEtapa.AvaliacaoId);
            ViewData["DisciplinaId"] =
                new SelectList(_context.Disciplinas, "Id", "Nome", avaliacaoDisciplinaEtapa.DisciplinaId);
            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome", avaliacaoDisciplinaEtapa.EtapaId);

            return View(avaliacaoDisciplinaEtapa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int disciplinaId, int etapaId,
            AvaliacaoDisciplinaEtapa avaliacaoDisciplinaEtapa)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());
            avaliacaoDisciplinaEtapa.AvaliacaoId = avaliacao.Id;

            try
            {
                _context.Update(avaliacaoDisciplinaEtapa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool avaliacaoDisciplinaEtapaExists = _context.AvaliacaoDisciplinasEtapas.Any(
                    x => x.AvaliacaoId == avaliacao.Id &&
                         x.DisciplinaId == disciplinaId &&
                         x.EtapaId == etapaId);

                if (!avaliacaoDisciplinaEtapaExists)
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // public async Task<IActionResult> Delete(int disciplinaId, int etapaId)
        // {
        //     var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());
        //
        //     var avaliacaoDisciplinaEtapa = await _context.AvaliacaoDisciplinasEtapas
        //         .AsNoTracking()
        //         .FirstOrDefaultAsync(
        //             x => x.AvaliacaoId == avaliacao.Id &&
        //                  x.DisciplinaId == disciplinaId &&
        //                  x.EtapaId == etapaId
        //         );
        //
        //     if (avaliacaoDisciplinaEtapa is null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(avaliacaoDisciplinaEtapa);
        // }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int disciplinaId, int etapaId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var avaliacaoDisciplinaEtapa =
                await _context.AvaliacaoDisciplinasEtapas.FirstOrDefaultAsync(
                    x => x.AvaliacaoId == avaliacao.Id &&
                         x.DisciplinaId == disciplinaId &&
                         x.EtapaId == etapaId
                );

            _context.Remove(avaliacaoDisciplinaEtapa);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}