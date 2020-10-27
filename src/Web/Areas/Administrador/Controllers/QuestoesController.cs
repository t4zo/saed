using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class QuestoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public QuestoesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.View)]
        public async Task<IActionResult> Index(int? disciplinaId, int? temaId, int? descritorId)
        {
            var disciplinas = await _context.Disciplinas.AsNoTracking().ToListAsync();

            var questoes = await _context.Questoes
                .AsNoTracking()
                .Include("Descritor.Tema.Disciplina")
                .ToListAsync();

            if (disciplinaId.HasValue)
            {
                questoes = questoes.Where(x => x.Descritor.Tema.DisciplinaId == disciplinaId.Value).ToList();

                //var temas = questoes.Select(x => x.Descritor.Tema).Distinct();
                var temas = await _context.Temas.AsNoTracking().Where(x => x.DisciplinaId == disciplinaId.Value).Distinct().ToListAsync();
                ViewBag.Temas = new SelectList(temas, "Id", "Nome", temaId);

                if (temaId.HasValue)
                {
                    questoes = questoes.Where(x => x.Descritor.TemaId == temaId.Value).ToList();

                    //var descritores = questoes.Select(x => x.Descritor).Distinct();
                    var descritores = await _context.Descritores.AsNoTracking().Where(x => x.TemaId == temaId.Value).Distinct().ToListAsync();
                    ViewBag.Descritores = new SelectList(descritores, "Id", "Nome", descritorId);

                    if (descritorId.HasValue)
                    {
                        questoes = questoes.Where(d => d.DescritorId == descritorId.Value).ToList();
                    }
                }
            }

            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome", disciplinaId);

            return View(questoes);

        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        public IActionResult CreateAsync()
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Questao questao)
        {
            if (ModelState.IsValid)
            {
                var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");

                var _questao = await _context.Questoes.AddAsync(questao);
                await _context.SaveChangesAsync();

                if (questao.Habilitada)
                {
                    await _context.AvaliacaoQuestoes.AddAsync(new AvaliacaoQuestao { AvaliacaoId = avaliacao.Id, QuestaoId = _questao.Entity.Id });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DescritorId"] = new SelectList(_context.Descritores, "Id", "Nome", questao.DescritorId);

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var questao = await _context.Questoes
                .AsNoTracking()
                .Include("Descritor.Tema.Disciplina")
                .FirstOrDefaultAsync(x => x.Id == id);

            var temaId = questao.Descritor.TemaId;
            var disciplinaId = questao.Descritor.Tema.DisciplinaId;

            if (questao is null)
            {
                return NotFound();
            }

            var disciplinas = await _context.Disciplinas.ToListAsync();
            var temas = await _context.Temas.Include(x => x.Descritores).Where(x => x.DisciplinaId == disciplinaId).Distinct().ToListAsync();
            var descritores = temas.Where(x => x.Id == temaId).SelectMany(x => x.Descritores).Distinct();

            ViewData["DisciplinaId"] = new SelectList(disciplinas, "Id", "Nome", questao.Descritor.Tema.DisciplinaId);
            ViewData["TemaId"] = new SelectList(temas, "Id", "Nome", questao.Descritor.TemaId);
            ViewData["DescritorId"] = new SelectList(descritores, "Id", "Nome", questao.Descritor.TemaId);

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
                var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");
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

                var avaliacaoQuestao = await _context.AvaliacaoQuestoes.Include(x => x.Questao).FirstOrDefaultAsync(x => x.AvaliacaoId == avaliacao.Id && x.QuestaoId == questao.Id);

                if (avaliacaoQuestao is null)
                {
                    if (questao.Habilitada)
                    {
                        await _context.AvaliacaoQuestoes.AddAsync(new AvaliacaoQuestao { AvaliacaoId = avaliacao.Id, QuestaoId = questao.Id });
                    }
                }
                else
                {
                    if (!questao.Habilitada)
                    {
                        _context.Remove(avaliacaoQuestao);
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DescritorId"] = new SelectList(_context.Descritores, "Id", "Nome", questao.DescritorId);

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var questao = await _context.Questoes.Include("Descritor.Tema.Disciplina").FirstOrDefaultAsync(x => x.Id == id);

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
