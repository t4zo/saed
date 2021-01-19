namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoEtapaViewModel
    {
        public EtapaViewModel EtapaViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public TemaViewModel TemaViewModel { get; set; }
        //public DescritorViewModel DescritorViewModel { get; set; }
        public double TaxaAcertoDisciplina => (double) DisciplinaViewModel.QtdRespostasCorretas / (DisciplinaViewModel.QtdQuestoesDisciplina* EtapaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoTema => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoesTema * EtapaViewModel.QtdAlunos) * 100;
    }
}