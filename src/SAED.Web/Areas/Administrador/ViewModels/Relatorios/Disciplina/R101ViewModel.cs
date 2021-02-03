using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R101ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<EtapaViewModel> EtapasViewModel { get; set; }
        public IList<DisciplinaViewModel> DisciplinasViewModel { get; set; }
        public IList<ResultadoEtapaViewModel> ResultadoEtapasViewModel { get; set; }

        public R101ViewModel()
        {
            EtapasViewModel = new List<EtapaViewModel>();
            DisciplinasViewModel = new List<DisciplinaViewModel>();
            ResultadoEtapasViewModel = new List<ResultadoEtapaViewModel>();
        }
    }
}