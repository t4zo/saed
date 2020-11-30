namespace SAED.Core.Entities
{
    public class Alternativa : BaseEntity
    {
        public string Descricao { get; set; }
        public bool Correta { get; set; }
        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }
        public RespostaAluno RespostaAluno { get; set; }
    }
}