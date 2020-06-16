using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace Web.Areas.Administrador.Controllers
{
    [Authorize]
    [Area("Administrador")]
    public class DashboardController : Controller
    {
        [Authorize(Permissions.Users.View)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Permissions.Users.Test)]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
