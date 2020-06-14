namespace SAED.ApplicationCore.Entities
{
    public class Alternativa : BaseEntity
    {
        public string Descricao { get; set; }
        public int QuestaoId { get; set; }
        public bool Correta { get; set; }
        public Questao Questao { get; set; }
        public RespostaAluno RespostaAlunos { get; set; }
    }
}