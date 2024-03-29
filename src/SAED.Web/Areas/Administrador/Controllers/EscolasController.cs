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
    public class EscolasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EscolasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.View)]
        public async Task<IActionResult> Index()
        {
            var escolas = await _context.Escolas
                .AsNoTracking()
                .Include(x => x.Distrito)
                .ToListAsync();

            return View(escolas);
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Create)]
        public async Task<IActionResult> Create()
        {
            var escolas = await _context.Escolas
                .AsNoTracking()
                .Include(x => x.Distrito)
                .ToListAsync();

            var distritos = escolas.Select(x => x.Distrito).Distinct().ToList();

            ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome");
            ViewData["MatrizId"] = new SelectList(escolas, "Id", "Nome");

            return View();
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escola escola)
        {
            if (!ModelState.IsValid)
            {
                var escolas = await _context.Escolas
                    .AsNoTracking()
                    .Include(x => x.Distrito)
                    .ToListAsync();

                var distritos = escolas.Select(x => x.Distrito).Distinct().ToList();
                ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome", escola.DistritoId);
                ViewData["MatrizId"] = new SelectList(escolas, "Id", "Email", escola.MatrizId);

                return View(escola);
            }

            await _context.AddAsync(escola);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Update)]
        public async Task<IActionResult> Edit(int id)
        {
            var distritos = await _context.Distritos
                .AsNoTracking()
                .Include(x => x.Escolas)
                .ToListAsync();

            var escola = distritos.SelectMany(x => x.Escolas).FirstOrDefault(x => x.Id == id);

            if (escola is null)
            {
                return NotFound();
            }

            ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome", escola.DistritoId);
            ViewData["MatrizId"] = new SelectList(distritos.SelectMany(x => x.Escolas), "Id", "Nome", escola.MatrizId);

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

            if (!ModelState.IsValid)
            {
                var distritos = await _context.Distritos.Include(x => x.Escolas).ToListAsync();

                ViewData["DistritoId"] = new SelectList(distritos, "Id", "Nome", escola.DistritoId);
                ViewData["MatrizId"] = new SelectList(distritos.SelectMany(x => x.Escolas), "Id", "Email", escola.MatrizId);

                return View(escola);
            }

            try
            {
                _context.Update(escola);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var entity = await _context.Escolas.FindAsync(id);
                if (entity is null)
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var escola = await _context.Escolas.FindAsync(id);
            if (escola is null)
            {
                return NotFound();
            }

            return View(escola);
        }

        [Authorize(AuthorizationConstants.Permissions.Escolas.Delete)]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var escola = await _context.Escolas.FindAsync(id);

            if (escola is null)
            {
                return NotFound();
            }

            _context.Remove(escola);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}