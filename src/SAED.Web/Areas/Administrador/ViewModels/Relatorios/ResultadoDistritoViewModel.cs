namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoDistritoViewModel
    {
        public DistritoViewModel DistritoViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public TemaViewModel TemaViewModel { get; set; }
        public DescritorViewModel DescritorViewModel { get; set; }
        public double TaxaAcertoDisciplina => (double) DisciplinaViewModel.QtdRespostasCorretas / (DisciplinaViewModel.QtdQuestoes * DistritoViewModel.QtdAlunos) * 100;
        public double TaxaAcertoTema => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoes * DistritoViewModel.QtdAlunos) * 100;
        public double TaxaAcertoDescritor => (double) DescritorViewModel.QtdRespostasCorretas / (DescritorViewModel.QtdQuestoes * DistritoViewModel.QtdAlunos) * 100;
    }
}