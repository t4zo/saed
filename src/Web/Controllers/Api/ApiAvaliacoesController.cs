using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SAED.Web.Controllers.Api
{
    public class ApiAvaliacoesController : BaseApiController
    {
        private readonly IAsyncRepository<Avaliacao> _avalicaoRepository;

        public ApiAvaliacoesController(IAsyncRepository<Avaliacao> avalicaoRepository)
        {
            _avalicaoRepository = avalicaoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _avalicaoRepository.ListAllAsync();
            return Ok(avaliacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var avaliacaoSpec = new AvaliacoesWithSpecification(id);
            var avaliacoes = await _avalicaoRepository.FirstOrDefaultAsync(avaliacaoSpec);
            return Ok(avaliacoes);
        }
    }
}
