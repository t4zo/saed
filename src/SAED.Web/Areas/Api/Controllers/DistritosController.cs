using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DistritosController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DistritosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Distritos.View)]
        public async Task<ActionResult<IEnumerable<Distrito>>> Index()
        {
            var distritos = await _context.Distritos
                .AsNoTracking()
                .ToListAsync();

            if (distritos is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(distritos));
        }
    }
}