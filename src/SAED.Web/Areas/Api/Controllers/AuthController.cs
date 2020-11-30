using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SAED.Infrastructure.Identity;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SAED.Web.Areas.Api.Controllers
{
    public class ApiAuthDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthController : BaseApiController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ApiAuthDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(new {success = true, message = "User logged!", user});
        }
    }
}