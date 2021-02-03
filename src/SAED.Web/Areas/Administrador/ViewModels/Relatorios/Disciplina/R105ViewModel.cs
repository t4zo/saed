using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R105ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<EscolaEtapaViewModel> EscolasEtapasViewModel { get; set; }
        public IList<DisciplinaViewModel> DisciplinasViewModel { get; set; }
        public IList<ResultadoEscolaEtapaViewModel> ResultadoEscolasEtapasViewModel { get; set; }

        public R105ViewModel()
        {
            EscolasEtapasViewModel = new List<EscolaEtapaViewModel>();
            DisciplinasViewModel = new List<DisciplinaViewModel>();
            ResultadoEscolasEtapasViewModel = new List<ResultadoEscolaEtapaViewModel>();
        }
    }
}