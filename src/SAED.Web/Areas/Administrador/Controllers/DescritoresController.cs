﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Persistence.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DescritoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DescritoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.View)]
        public async Task<IActionResult> Index(int? disciplinaId, int? temaId)
        {
            var disciplinas = await _context.Disciplinas.AsNoTracking().ToListAsync();

            var descritores = await _context.Descritores
                .AsNoTracking()
                .Include(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .ToListAsync();

            if (disciplinaId.HasValue)
            {
                descritores = descritores.Where(d => d.Tema.DisciplinaId == disciplinaId.Value).ToList();

                var temas = descritores.Select(x => x.Tema).Distinct().ToList();
                ViewBag.Temas = new SelectList(temas, "Id", "Nome", temaId);

                if (temaId.HasValue)
                {
                    descritores = descritores.Where(d => d.TemaId == temaId.Value).ToList();
                }
            }

            ViewBag.Disciplinas = new SelectList(disciplinas, "Id", "Nome", disciplinaId);

            return View(descritores);
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Create)]
        public IActionResult Create()
        {
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Descritor descritor)
        {
            if (descritor.TemaId == 0)
            {
                ModelState.AddModelError("Tema", "Tema Inválido");
                ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");

                return View(descritor);
            }

            if (!ModelState.IsValid)
            {
                ViewData["TemaId"] = new SelectList(_context.Temas, "Id", "Nome", descritor.TemaId);

                return View(descritor);
            }

            await _context.AddAsync(descritor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var descritor = await _context.Descritores
                .AsNoTracking()
                .Include(x => x.Tema)
                .ThenInclude(x => x.Disciplina)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (descritor is null)
            {
                return NotFound();
            }

            var temas = await _context.Temas
                .AsNoTracking()
                .Where(x => x.DisciplinaId == descritor.Tema.DisciplinaId)
                .ToListAsync();

            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", descritor.Tema.DisciplinaId);
            ViewData["TemaId"] = new SelectList(temas, "Id", "Nome", descritor.TemaId);

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

            if (descritor.TemaId == 0)
            {
                ModelState.AddModelError("Tema", "Tema Inválido");
                ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome");
                return View(descritor);
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Update(descritor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var entity = await _context.Descritores.FindAsync(id);
                if (entity is null)
                {
                    return NotFound();
                }

                throw;
            }

            var temas = await _context.Temas
                .AsNoTracking()
                .Include(x => x.Disciplina)
                .ToListAsync();
            ViewData["TemaId"] = new SelectList(temas, "Id", "Nome", descritor.TemaId);

            return View(descritor);
        }

        // [Authorize(AuthorizationConstants.Permissions.Descritores.Delete)]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var descritor = await _context.Descritores.FindAsync(id);
        //
        //     if (descritor is null)
        //     {
        //         return NotFound();
        //     }
        //
        //     ViewData["TemaId"] = new SelectList(await _context.Temas.AsNoTracking().Include("Tema.Disciplina").ToListAsync(), "Id", "Nome", descritor.TemaId);
        //
        //     return View(descritor);
        // }

        [Authorize(AuthorizationConstants.Permissions.Descritores.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var descritor = await _context.Descritores.FindAsync(id);

            if (descritor is null)
            {
                return NotFound();
            }

            _context.Remove(descritor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}