using System.Collections.Generic;
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
    public class AvaliacoesController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public AvaliacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Avaliacoes.View)]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> Index()
        {
            var avaliacoes = await _context.Avaliacoes
                .AsNoTracking()
                .ToListAsync();

            if (avaliacoes is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(avaliacoes));
        }
    }
}