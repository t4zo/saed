using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R301ViewModel
    {
        public R301ViewModel()
        {
            EtapasViewModel = new List<EtapaViewModel>();
            DescritoresViewModel = new List<DescritorViewModel>();
            ResultadoEtapasViewModel = new List<ResultadoEtapaViewModel>();
        }

        public int QtdTotalQuestoes { get; set; }
        public IList<EtapaViewModel> EtapasViewModel { get; set; }
        public IList<DescritorViewModel> DescritoresViewModel { get; set; }
        public IList<ResultadoEtapaViewModel> ResultadoEtapasViewModel { get; set; }
    }
}