using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R205ViewModel
    {
        public R205ViewModel()
        {
            EscolasEtapasViewModel = new List<EscolaEtapaViewModel>();
            TemasViewModel = new List<TemaViewModel>();
            ResultadoEscolasEtapasViewModel = new List<ResultadoEscolaEtapaViewModel>();
        }

        public int QtdTotalQuestoes { get; set; }
        public IList<EscolaEtapaViewModel> EscolasEtapasViewModel { get; set; }
        public IList<TemaViewModel> TemasViewModel { get; set; }
        public IList<ResultadoEscolaEtapaViewModel> ResultadoEscolasEtapasViewModel { get; set; }
    }
}