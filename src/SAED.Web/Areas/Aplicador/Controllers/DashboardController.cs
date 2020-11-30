using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAED.Core.Constants;
using SAED.Infrastructure.Data;
using SAED.Web.Areas.Aplicador.ViewModels;
using SAED.Web.Extensions;

namespace SAED.Web.Areas.Aplicador.Controllers
{
    [Area(AuthorizationConstants.Areas.Aplicador)]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [Authorize(AuthorizationConstants.Permissions.DashboardAplicador.View)]
        public IActionResult Index()
        {
            var alunoMetadata = HttpContext.Session.Get<ChooseAlunoRequest>("alunoMetadata");
            
            return View(alunoMetadata);
        }
    }
}