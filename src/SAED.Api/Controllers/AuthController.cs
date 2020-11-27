using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Api.Entities.Dto;
using SAED.Api.Entities.Responses;
using SAED.Api.Interfaces;
using SAED.ApplicationCore.Constants;
using SAED.Infrastructure.Identity;

namespace SAED.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public AuthController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticationRequest authenticationRequest)
        {
            UserResponse user = await _userService.AuthenticateAsync(authenticationRequest.Username,
                authenticationRequest.Password, authenticationRequest.Remember);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Success = true, Message = "Usuário conectado com sucesso!", User = user
            };

            return Ok(response);
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