using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R203ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<DistritoEtapaViewModel> DistritosEtapasViewModel { get; set; }
        public IList<TemaViewModel> TemasViewModel { get; set; }
        public IList<ResultadoDistritoEtapaViewModel> ResultadoDistritosEtapasViewModel { get; set; }

        public R203ViewModel()
        {
            DistritosEtapasViewModel = new List<DistritoEtapaViewModel>();
            TemasViewModel = new List<TemaViewModel>();
            ResultadoDistritosEtapasViewModel = new List<ResultadoDistritoEtapaViewModel>();
        }
    }
}