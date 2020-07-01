using SAED.ApplicationCore.Interfaces;

namespace SAED.ApplicationCore.Entities
{
    public class QuestaoAvaliacao : IManyToMany
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }
    }
}