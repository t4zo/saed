using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAED.Core.Constants;
using SAED.Persistence.Data;

namespace SAED.Web.Areas.Administrador.Controllers
{
    [Area(AuthorizationConstants.Areas.Administrador)]
    [Authorize(AuthorizationConstants.Permissions.Relatorios.View)]
    public partial class RelatoriosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}