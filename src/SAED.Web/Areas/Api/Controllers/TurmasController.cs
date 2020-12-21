using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
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
        public async Task<ActionResult<IEnumerable<Turma>>> Get(int etapaId)
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .Where(x => x.EtapaId == etapaId)
                .ToListAsync();

            if (turmas is null)
            {
                return BadRequest();
            }

            return Ok(JsonSerializer.Serialize(turmas));
        }
    }
}