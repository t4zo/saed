using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SAED.Web.Controllers
{
    [AllowAnonymous]
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}