using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SAED.Api.Controllers
{
    [AllowAnonymous]
    public class TesteController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetAll()
        {
            return Ok("Testando");
        }
    }
}
