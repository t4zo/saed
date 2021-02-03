using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R202ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<DistritoViewModel> DistritosViewModel { get; set; }
        public IList<TemaViewModel> TemasViewModel { get; set; }
        public IList<ResultadoDistritoViewModel> ResultadoDistritosViewModel { get; set; }

        public R202ViewModel()
        {
            DistritosViewModel = new List<DistritoViewModel>();
            TemasViewModel = new List<TemaViewModel>();
            ResultadoDistritosViewModel = new List<ResultadoDistritoViewModel>();
        }
    }
}