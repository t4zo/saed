using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R201ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<EtapaViewModel> EtapasViewModel { get; set; }
        public IList<TemaViewModel> TemasViewModel { get; set; }
        public IList<ResultadoEtapaViewModel> ResultadoEtapasViewModel { get; set; }

        public R201ViewModel()
        {
            EtapasViewModel = new List<EtapaViewModel>();
            TemasViewModel = new List<TemaViewModel>();
            ResultadoEtapasViewModel = new List<ResultadoEtapaViewModel>();
        }
    }
}