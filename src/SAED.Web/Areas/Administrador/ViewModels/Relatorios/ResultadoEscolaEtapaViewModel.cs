namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoEscolaEtapaViewModel
    {
        public EscolaEtapaViewModel EscolaEtapaViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public TemaViewModel TemaViewModel { get; set; }
        public DescritorViewModel DescritorViewModel { get; set; }
        public double TaxaAcertoDisciplina => (double) DisciplinaViewModel.QtdRespostasCorretas / (DisciplinaViewModel.QtdQuestoes * EscolaEtapaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoTema => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoes * EscolaEtapaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoDescritor => (double) DescritorViewModel.QtdRespostasCorretas / (DescritorViewModel.QtdQuestoes * EscolaEtapaViewModel.QtdAlunos) * 100;
    }
}