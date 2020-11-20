using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels
{
    public class GrupoViewModel
    {
        public string Nome { get; set; }
        public IList<string> PermissoesEscolhidas { get; set; }
    }
}