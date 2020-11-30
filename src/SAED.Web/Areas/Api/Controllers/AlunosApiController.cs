using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Infrastructure.Data;

namespace SAED.Web.Areas.Api.Controllers
{
    public class AlunosApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public AlunosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Get(int id)
        {
            var alunos = await _context.Alunos
                .AsNoTracking()
                .Where(x => x.TurmaId == id)
                .ToListAsync();

            if (alunos is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(alunos));
        }
    }
}