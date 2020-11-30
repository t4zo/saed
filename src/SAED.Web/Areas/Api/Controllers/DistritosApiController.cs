using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Web.Extensions;

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
            var distritos = await _context.Distritos.AsNoTracking().ToListAsync();
            
            if (distritos is null)
            {
                return NotFound();
            }

            return Ok(distritos);
        }
    }
}