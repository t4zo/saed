using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;

namespace SAED.Web.Areas.Api.Controllers
{
    public class TurmasApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public TurmasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Get(int id)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var turmas = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Etapa)
                    .ThenInclude(x => x.AvaliacaoDisciplinasEtapas
                    .Where(ade => ade.AvaliacaoId == avaliacao.Id))
                .Where(x => x.EtapaId == id)
                .ToListAsync();

            if (turmas is null)
            {
                return BadRequest();
            }
            
            foreach (var turma in turmas)
            {
                turma.Etapa = null;
            }

            return Ok(JsonSerializer.Serialize(turmas));
        }
    }
}