using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    public class AvaliacaoDisciplinasEtapasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvaliacaoDisciplinasEtapasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");
            var avaliacaoDisciplinasEtapas = await _context.AvaliacaoDisciplinasEtapas
                .Include(x => x.Avaliacao)
                .Include(x => x.Disciplina)
                .Include(x => x.Etapa)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .ToListAsync();

            return View(avaliacaoDisciplinasEtapas);
        }

        public IActionResult Create()
        {
            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Codigo");
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");
            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AvaliacaoDisciplinaEtapa avaliacaoDisciplinaEtapa)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");

            if (ModelState.IsValid)
            {
                var avaliacaoDisciplinaEtapaExists = await _context.AvaliacaoDisciplinasEtapas.AnyAsync(
                    x => x.AvaliacaoId == avaliacao.Id && 
                    x.DisciplinaId == avaliacaoDisciplinaEtapa.DisciplinaId && 
                    x.EtapaId == avaliacaoDisciplinaEtapa.EtapaId);

                if (!avaliacaoDisciplinaEtapaExists)
                {
                    avaliacaoDisciplinaEtapa.AvaliacaoId = avaliacao.Id;
                    _context.Add(avaliacaoDisciplinaEtapa);

                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

            return View(avaliacaoDisciplinaEtapa);
        }

        public async Task<IActionResult> Edit(int disciplinaId, int etapaId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");
            
            var avaliacaoDisciplinaEtapa = await _context.AvaliacaoDisciplinasEtapas
                .Include(x => x.Disciplina)
                .Include(x => x.Etapa)
                .FirstOrDefaultAsync(
                x => x.AvaliacaoId == avaliacao.Id &&
                x.DisciplinaId == disciplinaId &&
                x.EtapaId == etapaId);

            if (avaliacaoDisciplinaEtapa is null)
            {
                return NotFound();
            }

            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Codigo", avaliacaoDisciplinaEtapa.AvaliacaoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", avaliacaoDisciplinaEtapa.DisciplinaId);
            ViewData["EtapaId"] = new SelectList(_context.Etapas, "Id", "Nome", avaliacaoDisciplinaEtapa.EtapaId);

            return View(avaliacaoDisciplinaEtapa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int disciplinaId, int etapaId, AvaliacaoDisciplinaEtapa avaliacaoDisciplinaEtapa)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");

            if (ModelState.IsValid)
            {
                avaliacaoDisciplinaEtapa.AvaliacaoId = avaliacao.Id;
                try
                {
                    _context.Update(avaliacaoDisciplinaEtapa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var avaliacaoDisciplinaEtapaExists = _context.AvaliacaoDisciplinasEtapas.Any(
                        x => x.AvaliacaoId == avaliacao.Id && 
                        x.DisciplinaId == disciplinaId &&
                        x.EtapaId == etapaId);

                    if (!avaliacaoDisciplinaEtapaExists)
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

            ViewData["AvaliacaoId"] = new SelectList(_context.Avaliacoes, "Id", "Codigo", avaliacaoDisciplinaEtapa.AvaliacaoId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", avaliacaoDisciplinaEtapa.DisciplinaId);

            return View(avaliacaoDisciplinaEtapa);
        }

        public async Task<IActionResult> Delete(int disciplinaId, int etapaId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");

            var avaliacaoDisciplina = await _context.AvaliacaoDisciplinasEtapas.FirstOrDefaultAsync(
                x => x.AvaliacaoId == avaliacao.Id &&
                x.DisciplinaId == disciplinaId &&
                x.EtapaId == etapaId);

            if (avaliacaoDisciplina is null)
            {
                return NotFound();
            }

            return View(avaliacaoDisciplina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int disciplinaId, int etapaId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");

            var avaliacaoDisciplina = await _context.AvaliacaoDisciplinasEtapas.FirstOrDefaultAsync(
                x => x.AvaliacaoId == avaliacao.Id &&
                x.DisciplinaId == disciplinaId &&
                x.EtapaId == etapaId);

            _context.AvaliacaoDisciplinasEtapas.Remove(avaliacaoDisciplina);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
