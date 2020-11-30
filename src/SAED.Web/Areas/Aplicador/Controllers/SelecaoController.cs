using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAED.Core.Constants;
using SAED.Infrastructure.Data;

namespace SAED.Web.Areas.Aplicador.Controllers
{
    [Area(AuthorizationConstants.Areas.Aplicador)]
    public class SelecaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SelecaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(AuthorizationConstants.Permissions.Selecao.View)]
        public IActionResult Index()
        {
            ViewData["EscolaId"] = new SelectList(_context.Escolas, "Id", "Nome");
            return View();
        }
    }
}