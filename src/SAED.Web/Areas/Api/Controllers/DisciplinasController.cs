using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DisciplinasController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DisciplinasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Disciplinas.View)]
        public async Task<ActionResult<IEnumerable<Disciplina>>> Index()
        {
            var disciplinas = await _context.Disciplinas
                .AsNoTracking()
                .ToListAsync();

            if (disciplinas is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(disciplinas));
        }

        [Authorize(Permissions.Disciplinas.View)]
        public async Task<ActionResult<IEnumerable<Disciplina>>> Get(int id)
        {
            var disciplinas = await _context.Disciplinas.AsNoTracking().Where(x => x.Id == id).ToListAsync();

            if (disciplinas is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(disciplinas));
        }
    }
}