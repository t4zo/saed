using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels
{
    public class UsuarioViewModel
    {
        public IdentityUser<int> Usuario { get; set; }
        public string Password { get; set; }
        public IList<string> RolesEscolhidas { get; set; }
        public IList<string> PermissoesEscolhidas { get; set; }
    }
}
