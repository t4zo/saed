using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Infrastructure.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
            var temas = await _context.Temas.Where(x => x.DisciplinaId == id).ToListAsync();

            if (temas is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(temas));
        }
    }
}
