﻿namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoEtapaViewModel
    {
        public EtapaViewModel EtapaViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public TemaViewModel TemaViewModel { get; set; }
        public DescritorViewModel DescritorViewModel { get; set; }
        public double TaxaAcertoDisciplina => (double) DisciplinaViewModel.QtdRespostasCorretas / (DisciplinaViewModel.QtdQuestoes * EtapaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoTema => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoes * EtapaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoDescritor => (double) DescritorViewModel.QtdRespostasCorretas / (DescritorViewModel.QtdQuestoes * EtapaViewModel.QtdAlunos) * 100;
    }
}