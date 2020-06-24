using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAED.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("v1/[controller]")]
    public class AvaliacoesController : ControllerBase
    {
        private readonly IAsyncRepository<Avaliacao> _avaliacaoRepository;
        private readonly IUnityOfWork _uow;

        public AvaliacoesController(IAsyncRepository<Avaliacao> avaliacaoRepository, IUnityOfWork uow)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _uow = uow;
        }

        [HttpGet("")]
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

        [HttpPost("")]
        public async Task<ActionResult<Avaliacao>> Create(Avaliacao avaliacao)
        {
            await _avaliacaoRepository.AddAsync(avaliacao);
            await _uow.CommitAsync();

            return CreatedAtAction(nameof(Get), new { id = avaliacao.Id }, avaliacao);
        }

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
                await _uow.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Avaliacao>> Delete(int id)
        {
            var avaliacao = await _avaliacaoRepository.GetByIdAsync(id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            await _avaliacaoRepository.DeleteAsync(avaliacao);
            await _uow.CommitAsync();

            return Ok(avaliacao);
        }
    }
}
