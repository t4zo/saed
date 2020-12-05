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
    public class AlunosController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Permissions.Selecao.View)]
        public async Task<ActionResult<IEnumerable<Aluno>>> Get(int turmaId)
        {
            var alunos = await _context.Alunos
                .AsNoTracking()
                .Where(x => x.TurmaId == turmaId)
                .ToListAsync();

            if (alunos is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(alunos));
        }
    }
}