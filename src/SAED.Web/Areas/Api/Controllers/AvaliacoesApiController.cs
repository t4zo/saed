using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Infrastructure.Data;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Api.Controllers
{
    public class AvaliacoesApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public AvaliacoesApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Avaliacoes.View)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Avaliacoes.ToListAsync());
        }

        [Authorize(Permissions.Avaliacoes.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            return Ok(await _context.Avaliacoes.FindAsync(id));
        }
    }
}