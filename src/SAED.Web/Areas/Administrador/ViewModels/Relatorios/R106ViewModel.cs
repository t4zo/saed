using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class R106ViewModel
    {
        public int QtdTotalQuestoes { get; set; }
        public IList<AlunoViewModel> AlunosViewModel { get; set; }
        public IList<DisciplinaViewModel> DisciplinasViewModel { get; set; }
        public IList<ResultadoAlunoViewModel> ResultadoAlunosViewModel { get; set; }

        public R106ViewModel()
        {
            AlunosViewModel = new List<AlunoViewModel>();
            DisciplinasViewModel = new List<DisciplinaViewModel>();
            ResultadoAlunosViewModel = new List<ResultadoAlunoViewModel>();
        }
    }
}