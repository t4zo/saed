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
        public int QtdRespostasCorretas { get; set; }
        public int QtdQuestoes { get; set; }
        public int QtdAlunos { get; set; }
        public double TaxaAcerto => (double) QtdRespostasCorretas / (QtdQuestoes * QtdAlunos) * 100;
    }
}
