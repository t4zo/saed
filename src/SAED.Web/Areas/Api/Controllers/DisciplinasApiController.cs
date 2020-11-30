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
            var disciplinas = await _context.Disciplinas
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToListAsync();

            return Ok(disciplinas);
        }
    }
}