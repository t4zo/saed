using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DescritoresController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DescritoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Descritores.View)]
        public async Task<ActionResult<IEnumerable<Descritor>>> Get(int temaId)
        {
            var descritores = await _context.Descritores
                .AsNoTracking()
                .Where(x => x.TemaId == temaId)
                .ToListAsync();

            if (descritores is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(descritores));
        }
    }
}