using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R100ViewModel
    {
        public R100ViewModel()
        {
            ResultadoMunicipioViewModel = new List<ResultadoMunicipioViewModel>();
        }

        public int QtdTotalQuestoes { get; set; }
        public IList<ResultadoMunicipioViewModel> ResultadoMunicipioViewModel { get; set; }
    }
}
