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
    public class SalasController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public SalasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Descritores.View)]
        public async Task<ActionResult<IEnumerable<Sala>>> Get(int escolaId)
        {
            var salas = await _context.Salas
                .AsNoTracking()
                .Where(x => x.EscolaId == escolaId)
                .ToListAsync();

            if (salas is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(salas));
        }
    }
}