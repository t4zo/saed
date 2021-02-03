using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R303ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<DistritoEtapaViewModel> DistritosEtapasViewModel { get; set; }
        public IList<DescritorViewModel> DescritoresViewModel { get; set; }
        public IList<ResultadoDistritoEtapaViewModel> ResultadoDistritosEtapasViewModel { get; set; }

        public R303ViewModel()
        {
            DistritosEtapasViewModel = new List<DistritoEtapaViewModel>();
            DescritoresViewModel = new List<DescritorViewModel>();
            ResultadoDistritosEtapasViewModel = new List<ResultadoDistritoEtapaViewModel>();
        }
    }
}