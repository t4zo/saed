﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class TemasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TemasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.View)]
        public async Task<IActionResult> Index()
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>("avaliacao");

            var temas = await _context.Temas
                .AsNoTracking()
                .Include(x => x.Disciplina)
                .ToListAsync();
            return View(temas);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Create)]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.AsNoTracking().ToListAsync(), "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tema tema)
        {
            if (ModelState.IsValid)
            {
                await _context.Temas.AddAsync(tema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.AsNoTracking().ToListAsync(), "Id", "Nome", tema.DisciplinaId);

            return View(tema);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var tema = await _context.Temas.FindAsync(id);

            if (tema is null)
            {
                return NotFound();
            }

            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.AsNoTracking().ToListAsync(), "Id", "Nome", tema.DisciplinaId);

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
                    _context.Update(tema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var entity = await _context.Temas.FindAsync(id);
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

            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.AsNoTracking().ToListAsync(), "Id", "Nome", tema.DisciplinaId);

            return View(tema);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var tema = await _context.Temas.FindAsync(id);

            if (tema is null)
            {
                return NotFound();
            }

            return View(tema);
        }

        [Authorize(AuthorizationConstants.Permissions.Temas.Delete)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tema = await _context.Temas.FindAsync(id);
            _context.Remove(tema);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}