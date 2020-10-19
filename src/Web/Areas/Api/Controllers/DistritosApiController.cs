using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Infrastructure.Data;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DistritosApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DistritosApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var distritos = await _context.Distritos.ToListAsync();

            if (distritos is null)
            {
                return NotFound();
            }

            return Ok(distritos);
        }
    }
}
