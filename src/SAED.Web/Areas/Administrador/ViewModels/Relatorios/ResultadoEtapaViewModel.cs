﻿namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoEtapaViewModel
    {
        public EtapaViewModel EtapaViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public int QtdRespostasCorretas { get; set; }
        public int QtdQuestoes { get; set; }
        public int QtdAlunos { get; set; }
        public double TaxaAcerto => (double) QtdRespostasCorretas / (QtdQuestoes * QtdAlunos) * 100;
    }
}