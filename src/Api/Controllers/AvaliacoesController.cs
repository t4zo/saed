using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAED.Api.Controllers
{
    [Authorize(AuthorizationConstants.Permissions.Avaliacoes.View)]
    public class AvaliacoesController : ApiControllerBase
    {
        private readonly IAsyncRepository<Avaliacao> _avaliacaoRepository;
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(IAsyncRepository<Avaliacao> avaliacaoRepository, ApplicationDbContext context)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Avaliacao>>> GetAll()
        {
            return Ok(await _avaliacaoRepository.ListAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Avaliacao>> Get(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            return Ok(avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Create)]
        [HttpPost]
        public async Task<ActionResult<Avaliacao>> Create(Avaliacao avaliacao)
        {
            await _avaliacaoRepository.AddAsync(avaliacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = avaliacao.Id }, avaliacao);
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Avaliacao avaliacao)
        {
            if (id != avaliacao.Id)
            {
                return NotFound();
            }

            await _avaliacaoRepository.UpdateAsync(avaliacao);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [Authorize(AuthorizationConstants.Permissions.Avaliacoes.Delete)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Avaliacao>> Delete(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            await _avaliacaoRepository.DeleteAsync(avaliacao);
            await _context.SaveChangesAsync();

            return Ok(avaliacao);
        }
    }
}
