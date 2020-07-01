using SAED.ApplicationCore.Interfaces;

namespace SAED.ApplicationCore.Entities
{
    public class TurmaAluno : IManyToMany
    {
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
    }
}