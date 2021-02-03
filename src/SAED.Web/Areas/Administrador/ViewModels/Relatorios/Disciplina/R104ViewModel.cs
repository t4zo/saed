using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R104ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<EscolaViewModel> EscolasViewModel { get; set; }
        public IList<DisciplinaViewModel> DisciplinasViewModel { get; set; }
        public IList<ResultadoEscolaViewModel> ResultadoEscolasViewModel { get; set; }

        public R104ViewModel()
        {
            EscolasViewModel = new List<EscolaViewModel>();
            DisciplinasViewModel = new List<DisciplinaViewModel>();
            ResultadoEscolasViewModel = new List<ResultadoEscolaViewModel>();
        }
    }
}