using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Data;

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
            List<Distrito> distritos = await _context.Distritos.ToListAsync();

            if (distritos is null)
            {
                return NotFound();
            }

            return Ok(distritos);
        }
    }
}