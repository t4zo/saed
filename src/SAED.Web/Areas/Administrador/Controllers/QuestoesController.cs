using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Extensions;

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
        public async Task<IActionResult> Index(int? disciplinaId, int? temaId, int? descritorId, int? etapaId)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());
            var disciplinas = await _context.Disciplinas.AsNoTracking().ToListAsync();

            var questoes = await GetQuestoesAsync(etapaId).ToListAsync();

            if (disciplinaId.HasValue)
            {
                questoes = questoes.Where(x => x.Descritor.Tema.DisciplinaId == disciplinaId.Value).ToList();

                var temas = questoes.Select(x => x.Descritor.Tema).Distinct().ToList();
                ViewBag.Temas = new SelectList(temas, "Id", "Nome", temaId);

                if (temaId.HasValue)
                {
                    questoes = questoes.Where(x => x.Descritor.TemaId == temaId.Value).ToList();

                    var descritores = questoes.Select(x => x.Descritor).Distinct().ToList();
                    ViewBag.Descritores = new SelectList(descritores, "Id", "Nome", descritorId);

                    if (descritorId.HasValue)
                    {
                        questoes = questoes.Where(d => d.DescritorId == descritorId.Value).ToList();
                    }
                }
            }

            var etapas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Etapa)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Etapa)
                .Distinct()
                .ToListAsync();

            ViewBag.Etapas = new SelectList(etapas, "Id", "Nome", etapaId);
            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome", disciplinaId);

            return View(questoes);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        public async Task<IActionResult> Create()
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var etapas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Etapa)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Etapa)
                .Distinct()
                .ToListAsync();

            ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome");
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestaoViewModel questaoViewModel)
        {
            if (ModelState.IsValid)
            {
                var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

                var etapas = await _context.AvaliacaoDisciplinasEtapas
                    .AsNoTracking()
                    .Include(x => x.Etapa)
                    .Where(x => x.AvaliacaoId == avaliacao.Id)
                    .Select(x => x.Etapa)
                    .Distinct()
                    .ToListAsync();

                var descritor = await _context.Descritores
                    .Include(x => x.Tema)
                    .ThenInclude(x => x.Disciplina)
                    .FirstOrDefaultAsync(x => x.Id == questaoViewModel.DescritorId);

                if (descritor is null)
                {
                    ModelState.AddModelError("Descritor", "Descritor inválido");

                    ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome");
                    ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

                    return View(questaoViewModel);
                }

                var entity = await _context.AvaliacaoDisciplinasEtapas
                    .FirstOrDefaultAsync(x =>
                        x.AvaliacaoId == avaliacao.Id &&
                        x.DisciplinaId == descritor.Tema.DisciplinaId &&
                        x.EtapaId == questaoViewModel.EtapaId);

                if (entity is null)
                {
                    ModelState.AddModelError("", "Etapa e/ou Disciplina inválida(s)");
                    ModelState.AddModelError("EtapaDisciplina", "Etapa e/ou Disciplina inválida(s)");

                    ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome", questaoViewModel.EtapaId);
                    ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

                    return View(questaoViewModel);
                }

                var questao = _mapper.Map<Questao>(questaoViewModel);
                await _context.Questoes.AddAsync(questao);
                await _context.SaveChangesAsync();

                if (questao.Habilitada)
                {
                    await _context.AvaliacaoQuestoes.AddAsync(new AvaliacaoQuestao
                    {
                        AvaliacaoId = avaliacao.Id, QuestaoId = questao.Id
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["DescritorId"] = new SelectList(_context.Descritores, "Id", "Nome", questaoViewModel.DescritorId);

            return View(questaoViewModel);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var questao = await _context.Questoes
                .AsNoTracking()
                .Include(x => x.Descritor)
                .ThenInclude(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .Include(x => x.Alternativas)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (questao is null)
            {
                return NotFound();
            }

            var temaId = questao.Descritor.TemaId;
            var disciplinaId = questao.Descritor.Tema.DisciplinaId;

            var disciplinas = await _context.Disciplinas.ToListAsync();

            var temas = await _context.Temas
                .Include(x => x.Descritores)
                .Where(x => x.DisciplinaId == disciplinaId)
                .Distinct()
                .ToListAsync();

            var descritores = temas.Where(x => x.Id == temaId)
                .SelectMany(x => x.Descritores)
                .Distinct()
                .ToList();

            var etapas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Etapa)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Etapa)
                .Distinct()
                .ToListAsync();

            ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome");
            ViewData["DisciplinaId"] = new SelectList(disciplinas, "Id", "Nome", questao.Descritor.Tema.DisciplinaId);
            ViewData["TemaId"] = new SelectList(temas, "Id", "Nome", questao.Descritor.TemaId);
            ViewData["DescritorId"] = new SelectList(descritores, "Id", "Nome", questao.Descritor.TemaId);

            var questaoViewModel = _mapper.Map<QuestaoViewModel>(questao);
            return View(questaoViewModel);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Update)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuestaoViewModel questaoViewModel)
        {
            if (id != questaoViewModel.Id)
            {
                return NotFound();
            }

            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var descritor = await _context.Descritores.Include("Tema.Disciplina")
                .FirstOrDefaultAsync(x => x.Id == questaoViewModel.DescritorId);

            if (descritor is null)
            {
                ModelState.AddModelError("Descritor", "Descritor inválido");

                var etapas = await _context.AvaliacaoDisciplinasEtapas
                    .AsNoTracking()
                    .Include(x => x.Etapa)
                    .Where(x => x.AvaliacaoId == avaliacao.Id)
                    .Select(x => x.Etapa)
                    .Distinct()
                    .ToListAsync();

                ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome", questaoViewModel.EtapaId);
                ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

                return View(questaoViewModel);
            }

            var avaliacaoDisciplinaEtapaExists = await _context.AvaliacaoDisciplinasEtapas
                .AnyAsync(x =>
                    x.AvaliacaoId == avaliacao.Id &&
                    x.DisciplinaId == descritor.Tema.DisciplinaId &&
                    x.EtapaId == questaoViewModel.EtapaId);
            if (!avaliacaoDisciplinaEtapaExists)
            {
                ModelState.AddModelError("", "Etapa e/ou Disciplina inválida(s)");
                ModelState.AddModelError("EtapaDisciplina", "Etapa e/ou Disciplina inválida(s)");

                var etapas = await _context.AvaliacaoDisciplinasEtapas
                    .AsNoTracking()
                    .Include(x => x.Etapa)
                    .Where(x => x.AvaliacaoId == avaliacao.Id)
                    .Select(x => x.Etapa)
                    .Distinct()
                    .ToListAsync();

                ViewData["EtapaId"] = new SelectList(etapas, "Id", "Nome", questaoViewModel.EtapaId);
                ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

                return View(questaoViewModel);
            }


            if (!ModelState.IsValid)
            {
                ViewData["DescritorId"] =
                    new SelectList(_context.Descritores, "Id", "Nome", questaoViewModel.DescritorId);

                return View(questaoViewModel);
            }

            var questao = _mapper.Map<Questao>(questaoViewModel);

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

                throw;
            }

            var avaliacaoQuestao = await _context.AvaliacaoQuestoes.Include(x => x.Questao)
                .FirstOrDefaultAsync(x => x.AvaliacaoId == avaliacao.Id && x.QuestaoId == questao.Id);

            if (avaliacaoQuestao is null)
            {
                if (questao.Habilitada)
                {
                    await _context.AvaliacaoQuestoes.AddAsync(new AvaliacaoQuestao
                    {
                        AvaliacaoId = avaliacao.Id, QuestaoId = questao.Id
                    });
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

        [Authorize(AuthorizationConstants.Permissions.Questoes.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var questao = await _context.Questoes.Include("Descritor.Tema.Disciplina")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (questao is null)
            {
                return NotFound();
            }

            return View(questao);
        }

        [Authorize(AuthorizationConstants.Permissions.Questoes.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questao = await _context.Questoes.FindAsync(id);

            if (questao is null)
            {
                return NotFound();
            }

            _context.Remove(questao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private IQueryable<Questao> GetQuestoesAsync(int? etapaId)
        {
            if (etapaId.HasValue)
            {
                return _context.Questoes
                    .AsNoTracking()
                    .Include(x => x.Descritor)
                    .ThenInclude(x => x.Tema)
                    .ThenInclude(x => x.Disciplina)
                    .Include(x => x.Etapa)
                    .Where(x => x.EtapaId == etapaId);
            }

            return _context.Questoes
                .AsNoTracking()
                .Include(x => x.Descritor)
                .ThenInclude(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .Include(x => x.Etapa);
        }
    }
}