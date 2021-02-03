using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R200ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<ResultadoMunicipioViewModel> ResultadoMunicipioViewModel { get; set; }

        public R200ViewModel()
        {
            ResultadoMunicipioViewModel = new List<ResultadoMunicipioViewModel>();
        }
    }
}