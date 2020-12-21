using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels
{
    public class RespostasAlunoViewModel
    {
        public int AvaliacaoId { get; set; }
        public int AlunoId { get; set; }
        public IList<int> AlternativasId { get; set; }
    }
}