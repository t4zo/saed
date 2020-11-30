using System.Collections.Generic;
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
    public class DescritoresApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DescritoresApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            // var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());

            // var descritores = await _context.Descritores
            //     .AsNoTracking()
            //     .Include(x => x.Tema)
            //     .ThenInclude(x => x.Disciplina)
            //     .ThenInclude(x => x.AvaliacaoDisciplinasEtapas.Where(y => y.AvaliacaoId == avaliacao.Id))
            //     .Where(x => x.TemaId == id)
            //     .ToListAsync();
            
            var descritores = await _context.Descritores
                .AsNoTracking()
                .Where(x => x.TemaId == id)
                .ToListAsync();

            if (descritores is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(descritores));
        }
    }
}