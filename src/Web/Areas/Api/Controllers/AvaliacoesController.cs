using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Api.Controllers
{
    public class AvaliacoesController : BaseApiController
    {
        private readonly IAsyncRepository<Avaliacao> _avalicaoRepository;

        public AvaliacoesController(IAsyncRepository<Avaliacao> avalicaoRepository)
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
