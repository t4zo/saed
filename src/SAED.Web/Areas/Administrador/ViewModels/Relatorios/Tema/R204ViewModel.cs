using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R204ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<EscolaViewModel> EscolasViewModel { get; set; }
        public IList<TemaViewModel> TemasViewModel { get; set; }
        public IList<ResultadoEscolaViewModel> ResultadoEscolasViewModel { get; set; }

        public R204ViewModel()
        {
            EscolasViewModel = new List<EscolaViewModel>();
            TemasViewModel = new List<TemaViewModel>();
            ResultadoEscolasViewModel = new List<ResultadoEscolaViewModel>();
        }
    }
}