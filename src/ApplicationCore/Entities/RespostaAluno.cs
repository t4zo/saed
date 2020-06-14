namespace SAED.ApplicationCore.Entities
{
    public class RespostaAluno
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int AlternativaId { get; set; }
        public Alternativa Alternativa { get; set; }
    }
}