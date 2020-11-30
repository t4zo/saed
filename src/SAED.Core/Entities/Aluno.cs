using System;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Aluno : BaseEntity
    {
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }
    }
}