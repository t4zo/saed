using Microsoft.AspNetCore.Mvc;
using SAED.Core.Constants;

namespace SAED.Web.Areas.Administrador.Controllers.Relatorios
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    public class R102Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}