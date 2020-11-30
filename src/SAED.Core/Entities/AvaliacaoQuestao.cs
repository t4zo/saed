using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class AvaliacaoQuestao : IManyToMany
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }
    }
}