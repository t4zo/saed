using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Persistence.Data;
using SAED.Web.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
            var avaliacao = HttpContext.Session.Get<Avaliacao>(SessionConstants.Avaliacao);

            var alunosRespondidos = await _context.RespostaAlunos
                .AsNoTracking()
                .Include(x => x.Aluno)
                //.ThenInclude(x => x.Turma)
                .Where(x => x.Aluno.TurmaId == turmaId && x.AvaliacaoId == avaliacao.Id)
                .Select(x => x.Aluno)
                .Distinct()
                .ToListAsync();

            if (alunosRespondidos is null)
            {
                return NotFound();
            }

            var todosAlunos = await _context.Alunos
                .AsNoTracking()
                .Where(x => x.TurmaId == turmaId)
                .ToListAsync();

            var alunos = todosAlunos.Except(alunosRespondidos).ToList();

            return Ok(JsonSerializer.Serialize(alunos));
        }
    }
}