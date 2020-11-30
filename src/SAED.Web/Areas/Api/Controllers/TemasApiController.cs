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
    public class TemasApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public TemasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var temas = await _context.Temas
                .AsNoTracking()
                .Where(x => x.DisciplinaId == id)
                .ToListAsync();

            if (temas is null)
            {
                return NotFound();
            }
            
            foreach (var tema in temas)
            {
                tema.Disciplina = null;
            }

            return Ok(JsonSerializer.Serialize(temas));
        }
    }
}