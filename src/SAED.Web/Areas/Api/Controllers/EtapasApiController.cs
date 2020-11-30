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
            var etapas = await _context.Escolas
                .AsNoTracking()
                .Include(x => x.Salas)
                .ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Etapa)
                .Where(x => x.Id == id)
                .SelectMany(x => x.Salas.SelectMany(y => y.Turmas.Select(z => z.Etapa)))
                .ToListAsync();

            if (etapas.Count == 0)
            {
                return NotFound();
            }
            
            return Ok(JsonSerializer.Serialize(etapas));
        }
    }
}