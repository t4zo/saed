using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAED.ApplicationCore.Constants;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Authorize(Permissions.Dashboard.View)]
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
