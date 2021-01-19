namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoAlunoViewModel
    {
        public AlunoViewModel AlunoViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public double TaxaAcertoDisciplina => (double) DisciplinaViewModel.QtdRespostasCorretas / DisciplinaViewModel.QtdQuestoes * 100;
        //public double TaxaAcertoTema => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoes * DistritoViewModel.QtdAlunos) * 100;
    }
}