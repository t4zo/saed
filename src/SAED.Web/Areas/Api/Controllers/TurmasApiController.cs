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
    public class TurmasApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public TurmasApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Get(int id)
        {
            var turmas = await _context.Turmas
                .AsNoTracking()
                .Where(x => x.EtapaId == id)
                .ToListAsync();

            if (turmas is null)
            {
                return BadRequest();
            }

            return Ok(JsonSerializer.Serialize(turmas));
        }
    }
}