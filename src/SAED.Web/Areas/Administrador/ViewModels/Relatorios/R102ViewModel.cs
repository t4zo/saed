using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R103ViewModel
    {
        public R103ViewModel()
        {
            DistritosEtapasViewModel = new List<DistritoEtapaViewModel>();
            DisciplinasViewModel = new List<DisciplinaViewModel>();
            ResultadoDistritosEtapasViewModel = new List<ResultadoDistritoEtapaViewModel>();
        }

        public int QtdTotalQuestoes { get; set; }
        public IList<DistritoEtapaViewModel> DistritosEtapasViewModel { get; set; }
        public IList<DisciplinaViewModel> DisciplinasViewModel { get; set; }
        public IList<ResultadoDistritoEtapaViewModel> ResultadoDistritosEtapasViewModel { get; set; }
    }
}