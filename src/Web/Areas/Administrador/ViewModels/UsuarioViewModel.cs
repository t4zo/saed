using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

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