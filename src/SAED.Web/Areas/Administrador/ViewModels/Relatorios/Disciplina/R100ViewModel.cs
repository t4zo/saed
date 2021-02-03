using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R100ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<ResultadoMunicipioViewModel> ResultadoMunicipioViewModel { get; set; }

        public R100ViewModel()
        {
            ResultadoMunicipioViewModel = new List<ResultadoMunicipioViewModel>();
        }
    }
}