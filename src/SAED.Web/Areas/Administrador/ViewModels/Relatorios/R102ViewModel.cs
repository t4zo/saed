using SAED.Core.Entities;
using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R102ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<DistritoViewModel> DistritosViewModel { get; set; }
        public IList<DisciplinaViewModel> DisciplinasViewModel { get; set; }
        public IList<ResultadoDistritoDisciplinaViewModel> ResultadoDistritosDisciplinasView { get; set; }

        public R102ViewModel()
        {
            DistritosViewModel = new List<DistritoViewModel>();
            DisciplinasViewModel = new List<DisciplinaViewModel>();
            ResultadoDistritosDisciplinasView = new List<ResultadoDistritoDisciplinaViewModel>();
        }
    }
}