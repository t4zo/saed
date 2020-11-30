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
    public class EtapasApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public EtapasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Get(int id)
        {
            var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            var etapas = await _context.Escolas.AsNoTracking()
                .Include(x => x.Salas)
                .ThenInclude(x => x.Turmas)
                    .ThenInclude(x => x.Etapa)
                        .ThenInclude(x => x.AvaliacaoDisciplinasEtapas.Where(y => y.AvaliacaoId == avaliacao.Id))
                .Where(x => x.Id == id)
                .SelectMany(x => x.Salas.SelectMany(y => y.Turmas.Select(z => z.Etapa)))
                .ToListAsync();
            
            foreach (var etapa in etapas)
            {
                etapa.AvaliacaoDisciplinasEtapas = null;
            }
            
            if (etapas.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(JsonSerializer.Serialize(etapas));
        }
    }
}