using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAED.ApplicationCore.Constants;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DashboardController : Controller
    {
        [Authorize(Permissions.Users.View)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
