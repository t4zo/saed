using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);

            var allEtapas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Etapa)
                .ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Sala)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Etapa)
                .ToListAsync();

            var etapas = allEtapas.Distinct().ToList();

            foreach (var etapa in etapas)
            {
                etapa.ClearReferenceCycle();
            }

            return Ok(JsonSerializer.Serialize(etapas));
        }
    }
}