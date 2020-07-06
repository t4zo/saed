using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Api.Entities.Dto;
using SAED.Api.Interfaces;
using SAED.ApplicationCore.Constants;
using SAED.Infrastructure.Identity;
using System.Threading.Tasks;

namespace SAED.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var user = await _userService.AuthenticateAsync(authenticationRequest.Username, authenticationRequest.Password, authenticationRequest.Remember);

            if (user is null)
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

        [Authorize(Roles = AuthorizationConstants.Roles.Superuser)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userManager.Users.ToListAsync());
        }

        [Authorize(Roles = AuthorizationConstants.Roles.Superuser)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _userManager.FindByIdAsync(id));
        }
    }
}
