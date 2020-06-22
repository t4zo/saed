using ApplicationCore.Specifications;
using Ardalis.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class EscolasController : Controller
    {
        private readonly IAsyncRepository<Escola> _escolasRepository;
        private readonly IAsyncRepository<Distrito> _distritosRepository;
        private readonly IUnityOfWork _uow;

        public EscolasController(IAsyncRepository<Escola> escolasRepository, IAsyncRepository<Distrito> distritosRepository, IUnityOfWork uow)
        {
            _escolasRepository = escolasRepository;
            _distritosRepository = distritosRepository;
            _uow = uow;
        }

        [Authorize(Permissions.Escolas.View)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var specification = new EscolasWithSpecification();
            //specification.Includes.Append(x => x.Distrito);
            var escolas = await _escolasRepository.ListAsync(specification);

            return View(escolas);
        }

        [Authorize(Permissions.Escolas.View)]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var escolas = await _escolasRepository.GetByIdAsync(id);

            return View(escolas);
        }

        [Authorize(Permissions.Escolas.Create)]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.DistritoId = new SelectList(await _distritosRepository.ListAllAsync(), "Id", "Nome");
            ViewBag.MatrizId = new SelectList(await _escolasRepository.ListAllAsync(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Escola escola)
        {
            if (ModelState.IsValid)
            {
                await _escolasRepository.AddAsync(escola);

                await _uow.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.DistritoId = new SelectList(await _distritosRepository.ListAllAsync(), "Id", "Nome", escola.DistritoId);
            ViewBag.MatrizId = new SelectList(await _escolasRepository.ListAllAsync(), "Id", "Nome", escola.MatrizId);
            return View(escola);
        }

        [Authorize(Permissions.Escolas.Update)]
        [HttpGet("administrador/escolas/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var escola = await _escolasRepository.GetByIdAsync(id);
            if (escola == null)
            {
                return NotFound();
            }

            ViewBag.DistritoId = new SelectList(await _distritosRepository.ListAllAsync(), "Id", "Nome", escola.DistritoId);
            ViewBag.MatrizId = new SelectList(await _escolasRepository.ListAllAsync(), "Id", "Nome", escola.MatrizId);

            return View(escola);
        }

        [Authorize(Permissions.Escolas.Update)]
        [HttpPost]
        public async Task<IActionResult> Update(int id, Escola escola)
        {
            if (id != escola.Id)
            {
                return BadRequest();
            }

            await _escolasRepository.UpdateAsync(escola);

            try
            {
                await _uow.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _escolasRepository.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize(Permissions.Escolas.Delete)]
        public async Task<ActionResult<Escola>> Delete(int id)
        {
            var escola = await _escolasRepository.GetByIdAsync(id);
            if (escola == null)
            {
                return NotFound();
            }

            await _escolasRepository.DeleteAsync(escola);
            await _uow.CommitAsync();

            return escola;
        }
    }
}
