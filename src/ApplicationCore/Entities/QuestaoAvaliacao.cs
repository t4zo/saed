namespace SAED.ApplicationCore.Entities
{
    public class QuestaoAvaliacao
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }
    }
}