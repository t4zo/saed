using System;
using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Aluno : BaseEntity
    {
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<TurmaAluno> TurmaAlunos { get; set; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }
    }
}