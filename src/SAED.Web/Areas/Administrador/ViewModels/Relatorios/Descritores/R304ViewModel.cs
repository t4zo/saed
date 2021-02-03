using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R304ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<EscolaViewModel> EscolasViewModel { get; set; }
        public IList<DescritorViewModel> DescritoresViewModel { get; set; }
        public IList<ResultadoEscolaViewModel> ResultadoEscolasViewModel { get; set; }

        public R304ViewModel()
        {
            EscolasViewModel = new List<EscolaViewModel>();
            DescritoresViewModel = new List<DescritorViewModel>();
            ResultadoEscolasViewModel = new List<ResultadoEscolaViewModel>();
        }
    }
}