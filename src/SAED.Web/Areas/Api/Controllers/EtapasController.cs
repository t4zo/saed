using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Api.Controllers
{
    public class EtapasController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public EtapasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Selecao.View)]
        public async Task<ActionResult<IEnumerable<Etapa>>> Get(int escolaId)
        {
            var etapas = await _context.Escolas
                .AsNoTracking()
                .Include(x => x.Salas)
                .ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Etapa)
                .Where(x => x.Id == escolaId)
                .SelectMany(x => x.Salas.SelectMany(y => y.Turmas.Select(z => z.Etapa)))
                .ToListAsync();

            if (etapas is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(etapas));
        }
    }
}