using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;

namespace SAED.Api.Controllers
{
    public class AvaliacoesController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.View)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Avaliacao>>> GetAll()
        {
            return Ok(await _context.Avaliacoes.ToListAsync());
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.View)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> Get(int id)
        {
            Avaliacao avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao is null)
            {
                return NotFound();
            }

            return Ok(avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Create)]
        [HttpPost]
        public async Task<ActionResult<Avaliacao>> Create(Avaliacao avaliacao)
        {
            await _context.AddAsync(avaliacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new {id = avaliacao.Id}, avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            _context.Update(avaliacao);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Delete)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Avaliacao>> Delete(int id)
        {
            Avaliacao avaliacao = await _context.Avaliacoes.FindAsync(id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            _context.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return Ok(avaliacao);
        }
    }
}