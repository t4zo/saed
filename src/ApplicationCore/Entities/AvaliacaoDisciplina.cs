namespace SAED.ApplicationCore.Entities
{
    public class AvaliacaoDisciplina
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
