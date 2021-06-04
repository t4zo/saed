using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Api.Controllers
{
    public class TurmasController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public TurmasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Selecao.View)]
        public async Task<ActionResult<IEnumerable<Turma>>> Get(int escolaId, int etapaId)
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Sala)
                .Where(x => x.Sala.EscolaId == escolaId && x.EtapaId == etapaId)
                .ToListAsync();

            if (turmas is null)
            {
                return BadRequest();
            }

            foreach (var turma in turmas)
            {
                turma.ClearReferenceCycle();
            }

            return Ok(JsonSerializer.Serialize(turmas));
        }

        [Authorize(Permissions.Salas.View)]
        [HttpGet("{salaId}")]
        public async Task<ActionResult<IEnumerable<Turma>>> Get(int salaId)
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .Where(x => x.SalaId == salaId)
                .ToListAsync();

            if (turmas is null)
            {
                return BadRequest();
            }

            foreach (var turma in turmas)
            {
                turma.ClearReferenceCycle();
            }

            return Ok(JsonSerializer.Serialize(turmas));
        }
    }
}