using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAED.Api.Entities.Dto;
using SAED.Api.Interfaces;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationDto model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password, model.Remember);

            if (user == null)
            {
                return ValidationProblem("Usuário ou Senha Inválido(s)");
            }

            return Ok(new
            {
                success = true,
                message = "User logged!",
                user
            });
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _userService.GetAllAsync());
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Forbid();
        //    }

        //    var user = await _userService.GetByIdAsync(id);

        //    if (user == null)
        //        return NotFound();

        //    return Ok(user);
        //}
    }
}
