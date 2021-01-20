using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoEscolaViewModel
    {
        public EscolaViewModel EscolaViewModel { get; set; }
        public DisciplinaViewModel DisciplinaViewModel { get; set; }
        public TemaViewModel TemaViewModel { get; set; }
        public DescritorViewModel DescritorViewModel { get; set; }
        public double TaxaAcertoDisciplina => (double) DisciplinaViewModel.QtdRespostasCorretas / (DisciplinaViewModel.QtdQuestoes * EscolaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoTema => (double) TemaViewModel.QtdRespostasCorretas / (TemaViewModel.QtdQuestoes * EscolaViewModel.QtdAlunos) * 100;
        public double TaxaAcertoDescritor => (double) DescritorViewModel.QtdRespostasCorretas / (DescritorViewModel.QtdQuestoes * EscolaViewModel.QtdAlunos) * 100;
    }
}
