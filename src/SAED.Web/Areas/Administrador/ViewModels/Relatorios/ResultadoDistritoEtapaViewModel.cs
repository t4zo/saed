namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoDistritoEtapaViewModel
    {
        public DistritoEtapaViewModel DistritoEtapaViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public TemaViewModel TemaViewModel { get; set; }
        public double TaxaAcertoDisciplina => (double) DisciplinaViewModel.QtdRespostasCorretas / (DisciplinaViewModel.QtdQuestoes * DistritoEtapaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoTema => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoes * DistritoEtapaViewModel.QtdAlunos) * 100;
    }
}