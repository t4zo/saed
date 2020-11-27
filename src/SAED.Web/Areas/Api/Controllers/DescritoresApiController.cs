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
    public class DescritoresApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DescritoresApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            List<Descritor> descritores =
                await _context.Descritores.AsNoTracking().Where(x => x.TemaId == id).ToListAsync();

            if (descritores is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(descritores));
        }
    }
}