using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserManager<ApplicationUser> _userManager { get; set; }

        public AlunosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int? escolaId, int? etapaId)
        {
            var alunos = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Cpf)
                .Include(a => a.Turma)
                .ThenInclude(x => x.Etapa)
                .Include(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .ToListAsync();

            if (escolaId.HasValue)
            {
                alunos = alunos.Where(x => x.Turma.Sala.EscolaId == escolaId).ToList();
            }

            if (etapaId.HasValue)
            {
                alunos = alunos.Where(x => x.Turma.EtapaId == etapaId).ToList();
            }

            ViewBag.Escolas = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", escolaId);
            ViewBag.Etapas = new SelectList(_context.Etapas.OrderBy(x => x.Nome), "Id", "Nome", etapaId);

            return View(alunos);
        }

        public IActionResult Create()
        {
            ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            if (aluno.TurmaId == 0)
            {
                ModelState.AddModelError("", "Escola e/ou Sala e/ou Turma inválido(s)");

                ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome");
            }

            var turma = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == aluno.TurmaId);

            if (!Cpf.IsValid(aluno.Cpf.Codigo))
            {
                ModelState.AddModelError("", "CPF Inválido");

                ViewData["Erro"] = true;
                ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
                ViewData["SalaId"] = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome", turma.SalaId);
                ViewData["TurmaId"] = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);
                return View(aluno);
            }

            if (!ModelState.IsValid)
            {
                ViewData["Erro"] = true;
                if (ModelState.ContainsKey("Nascimento"))
                {
                    ModelState.AddModelError("", "Data de Nascimento Inválida");
                }
                else
                {
                    ModelState.AddModelError("", "Algum campo está inválido");
                }

                ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
                ViewData["SalaId"] = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome", turma.SalaId);
                ViewData["TurmaId"] = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);

                return View(aluno);
            }

            var user = new ApplicationUser { UserName = aluno.Cpf.Normalize(), Email = aluno.Cpf.Normalize() };
            var result = await _userManager.CreateAsync(user, aluno.Cpf.Normalize());

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email e/ou Senha Inválida(s)/Existente");

                ViewData["Erro"] = true;
                ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
                ViewData["SalaId"] = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome", turma.SalaId);
                ViewData["TurmaId"] = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);
                return View(aluno);
            }

            await _userManager.AddToRoleAsync(user, AuthorizationConstants.Roles.Aluno);

            _context.Add(aluno);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Cpf)
                .Include(a => a.Turma)
                .ThenInclude(x => x.Etapa)
                .Include(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == id);

            var salasEscola = await _context.Salas
                .AsNoTracking()
                .Where(x => x.EscolaId == aluno.Turma.Sala.EscolaId)
                .ToListAsync();

            var turmasSala = await _context.Turmas
                .AsNoTracking()
                .Where(x => x.SalaId == aluno.Turma.SalaId)
                .ToListAsync();

            if (aluno is null)
            {
                return NotFound();
            }

            ViewBag.EscolaId = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", aluno.Turma.Sala.EscolaId);
            ViewBag.SalaId = new SelectList(salasEscola, "Id", "Nome", aluno.Turma.SalaId);
            ViewData["TurmaId"] = new SelectList(turmasSala, "Id", "Nome", aluno.TurmaId);

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (aluno.TurmaId == 0)
            {
                ModelState.AddModelError("", "Escola e/ou Sala e/ou Turma inválido(s)");

                ViewData["Erro"] = true;
                ViewBag.EscolaId = new SelectList(_context.Escolas, "Id", "Nome");

                return View(aluno);
            }

            var turma = await _context.Turmas
                .AsNoTracking()
                .Include(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == aluno.TurmaId);

            if (!ModelState.IsValid)
            {
                ViewData["Erro"] = true;
                if (ModelState.ContainsKey("Nascimento"))
                {
                    ModelState.AddModelError("", "Data de Nascimento Inválida");
                }
                else
                {
                    ModelState.AddModelError("", "Algum campo está inválido");
                }

                ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
                ViewData["SalaId"] = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome", turma.SalaId);
                ViewData["TurmaId"] = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);

                return View(aluno);
            }

            if (!Cpf.IsValid(aluno.Cpf.Codigo))
            {
                ModelState.AddModelError("", "CPF Inválido");

                ViewData["Erro"] = true;
                ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
                ViewData["SalaId"] = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome", turma.SalaId);
                ViewData["TurmaId"] = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);
                return View(aluno);
            }

            try
            {
                var alunoOriginal = await _context.Alunos
                    .AsNoTracking()
                    .Include(x => x.Cpf)
                    .FirstOrDefaultAsync(x => x.Id == aluno.Id);

                aluno.Cpf.Id = alunoOriginal.Cpf.Id;

                _context.Update(aluno);
                await _context.SaveChangesAsync();


                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == alunoOriginal.Cpf.Normalize());

                await _userManager.SetEmailAsync(user, aluno.Cpf.Normalize());
                await _userManager.SetUserNameAsync(user, aluno.Cpf.Normalize());
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, aluno.Cpf.Normalize());

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Email e/ou Senha Inválida(s)/Existente");

                    ViewData["Erro"] = true;
                    ViewData["EscolaId"] = new SelectList(_context.Escolas.OrderBy(x => x.Nome), "Id", "Nome", turma.Sala.EscolaId);
                    ViewData["SalaId"] = new SelectList(_context.Salas.Where(x => x.EscolaId == turma.Sala.EscolaId).OrderBy(x => x.Nome), "Id", "Nome", turma.SalaId);
                    ViewData["TurmaId"] = new SelectList(_context.Turmas.Where(x => x.SalaId == turma.SalaId), "Id", "Nome", turma.Id);
                    return View(aluno);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Alunos.Any(e => e.Id == id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Cpf)
                .Include(a => a.Turma)
                .ThenInclude(x => x.Etapa)
                .Include(x => x.Turma)
                .ThenInclude(x => x.Sala)
                .ThenInclude(x => x.Escola)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (aluno is null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos
                .Include(x => x.Cpf)
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Alunos.Remove(aluno);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == aluno.Cpf.Codigo);
            await _userManager.DeleteAsync(user);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}