using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SAED.Api.Controllers
{
    [AllowAnonymous]
    public class HelloWorldController : ApiControllerBase
    {
        [HttpGet("/hello")]
        public ActionResult<string> GetAll()
        {
            return Ok("Hello, World!");
        }
    }
}