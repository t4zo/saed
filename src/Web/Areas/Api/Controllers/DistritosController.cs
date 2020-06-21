using Microsoft.AspNetCore.Mvc;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DistritosController : BaseApiController
    {
        private readonly IAsyncRepository<Distrito> _distritoRepository;

        public DistritosController(IAsyncRepository<Distrito> distritoRepository)
        {
            _distritoRepository = distritoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var distritos = await _distritoRepository.ListAllAsync();

            if (distritos == null) return BadRequest();

            return Ok(distritos);
        }
    }
}
