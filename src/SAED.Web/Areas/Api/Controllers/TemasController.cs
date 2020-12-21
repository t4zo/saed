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
    public class TemasController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public TemasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Temas.View)]
        public async Task<ActionResult<IEnumerable<Tema>>> Get(int disciplinaId)
        {
            var temas = await _context.Temas
                .AsNoTracking()
                .Where(x => x.DisciplinaId == disciplinaId)
                .ToListAsync();

            if (temas is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(temas));
        }
    }
}