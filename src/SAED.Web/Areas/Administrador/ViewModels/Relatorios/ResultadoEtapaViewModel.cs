namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoEtapaViewModel
    {
        public EtapaViewModel EtapaViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public TemaViewModel TemaViewModel { get; set; }
        //public DescritorViewModel DescritorViewModel { get; set; }
        public double TaxaAcerto => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoesTema * EtapaViewModel.QtdAlunos) * 100;
    }
}