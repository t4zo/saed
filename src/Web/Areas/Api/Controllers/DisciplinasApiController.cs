using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Infrastructure.Data;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DisciplinasApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DisciplinasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok(await _context.Temas.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _context.Disciplinas.Where(x => x.Id == id).ToListAsync());
        }
    }
}