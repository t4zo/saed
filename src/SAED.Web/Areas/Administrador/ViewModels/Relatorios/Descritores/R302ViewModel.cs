using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R302ViewModel
    {
        public R302ViewModel()
        {
            DistritosViewModel = new List<DistritoViewModel>();
            DescritoresViewModel = new List<DescritorViewModel>();
            ResultadoDistritosViewModel = new List<ResultadoDistritoViewModel>();
        }

        public int QtdTotalQuestoes { get; set; }
        public IList<DistritoViewModel> DistritosViewModel { get; set; }
        public IList<DescritorViewModel> DescritoresViewModel { get; set; }
        public IList<ResultadoDistritoViewModel> ResultadoDistritosViewModel { get; set; }
    }
}