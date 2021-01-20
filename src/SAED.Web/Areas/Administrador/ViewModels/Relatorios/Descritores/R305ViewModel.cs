using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R305ViewModel
    {
        public R305ViewModel()
        {
            EscolasEtapasViewModel = new List<EscolaEtapaViewModel>();
            DescritoresViewModel = new List<DescritorViewModel>();
            ResultadoEscolasEtapasViewModel = new List<ResultadoEscolaEtapaViewModel>();
        }

        public int QtdTotalQuestoes { get; set; }
        public IList<EscolaEtapaViewModel> EscolasEtapasViewModel { get; set; }
        public IList<DescritorViewModel> DescritoresViewModel { get; set; }
        public IList<ResultadoEscolaEtapaViewModel> ResultadoEscolasEtapasViewModel { get; set; }
    }
}