namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoAlunoViewModel
    {
        public AlunoViewModel AlunoViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public int QtdRespostasCorretas { get; set; }
        public int QtdQuestoes { get; set; }
        public double TaxaAcerto => (double) QtdRespostasCorretas / (QtdQuestoes) * 100;
    }
}