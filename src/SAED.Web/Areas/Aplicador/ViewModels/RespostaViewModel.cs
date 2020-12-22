using SAED.Core.Entities;
using System.Collections.Generic;

namespace SAED.Web.Areas.Aplicador.ViewModels
{
    public class RespostaViewModel
    {
        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }
        public int AvaliacaoId { get; set; }
        public int AlunoId { get; set; }
        public int AlternativaEscolhidaId { get; set; }
        public Alternativa AlternativaEscolhida { get; set; }
    }
}