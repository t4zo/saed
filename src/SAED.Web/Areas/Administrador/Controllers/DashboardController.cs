using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAED.Core.Constants;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class DashboardController : Controller
    {
        [Authorize(AuthorizationConstants.Permissions.Dashboard.View)]
        public IActionResult Index()
        {
            return View();
        }
    }
}