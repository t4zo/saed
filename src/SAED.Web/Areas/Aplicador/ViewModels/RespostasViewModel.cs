using System.Collections.Generic;

namespace SAED.Web.Areas.Aplicador.ViewModels
{
    public class RespostasViewModel
    {
        public int AvaliacaoId { get; set; }
        public int AlunoId { get; set; }
        public IList<RespostaViewModel> Respostas { get; set; }
    }
}