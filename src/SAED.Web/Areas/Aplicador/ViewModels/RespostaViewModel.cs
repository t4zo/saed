using SAED.Core.Entities;

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

        public void Populate(int avaliacaoId, int alunoId, Questao questao)
        {
            AvaliacaoId = avaliacaoId;
            AlunoId = alunoId;
            QuestaoId = questao.Id;
            Questao = questao;
        }
    }
}