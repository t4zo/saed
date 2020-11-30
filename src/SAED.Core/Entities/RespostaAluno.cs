using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class RespostaAluno : IManyToMany
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int AlternativaId { get; set; }
        public Alternativa Alternativa { get; set; }
    }
}