using Microsoft.AspNetCore.Mvc;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.Web.Controllers.Api;
using System.Threading.Tasks;

namespace Web.Controllers.Api
{
    [Route("v1/distritos")]
    public class ApiDistritosController : BaseApiController
    {
        private readonly IAsyncRepository<Distrito> _distritoRepository;

        public ApiDistritosController(IAsyncRepository<Distrito> distritoRepository)
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
