using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;

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
            List<Tema> temas = await _context.Temas.Where(x => x.DisciplinaId == id).ToListAsync();

            if (temas is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(temas));
        }
    }
}