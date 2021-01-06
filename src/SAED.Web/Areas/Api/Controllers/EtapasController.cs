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

            var escola = await _context.Escolas.AsNoTracking().Include(x => x.Salas).ThenInclude(x => x.Turmas).ThenInclude(x => x.Etapa).FirstOrDefaultAsync(x => x.Id == escolaId);
            var etapasEscola = escola.Salas.SelectMany(x => x.Turmas.Select(y => y.Etapa)).Distinct().ToList();

            var allAvaliacaoDisciplinasEtapas = await _context.AvaliacaoDisciplinasEtapas
                .AsNoTracking()
                .Include(x => x.Etapa)
                .ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Sala)
                .Where(x => x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Etapa)
                .ToListAsync();

            var avaliacaoDisciplinasEtapas = allAvaliacaoDisciplinasEtapas.Distinct().ToList();

            var etapas = new List<Etapa>();
            foreach (var avaliacaoDisciplinaEtapa in avaliacaoDisciplinasEtapas)
            {
                avaliacaoDisciplinaEtapa.ClearReferenceCycle();

                foreach (var etapaEscola in etapasEscola)
                {
                    etapaEscola.ClearReferenceCycle();

                    if (avaliacaoDisciplinaEtapa.Id == etapaEscola.Id)
                    {
                        etapas.Add(etapaEscola);
                    }
                }
            }


            return Ok(JsonSerializer.Serialize(etapas));
        }
    }
}