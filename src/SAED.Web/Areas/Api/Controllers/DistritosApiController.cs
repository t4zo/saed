using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DistritosApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DistritosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // var avaliacao = HttpContext.Session.Get<Avaliacao>(nameof(Avaliacao).ToLower());
            //
            // var distritos = await _context.Distritos
            //     .AsNoTracking()
            //     .Include(x => x.Escolas)
            //     .ThenInclude(x => x.Salas)
            //     .ThenInclude(x => x.Turmas)
            //     .ThenInclude(x => x.Etapa)
            //     .ThenInclude(x => x.AvaliacaoDisciplinasEtapas.Where(ade => ade.AvaliacaoId == avaliacao.Id))
            //     .ToListAsync();

            var distritos = await _context.Distritos.AsNoTracking().ToListAsync();
            
            if (distritos is null)
            {
                return NotFound();
            }

            return Ok(distritos);
        }
    }
}