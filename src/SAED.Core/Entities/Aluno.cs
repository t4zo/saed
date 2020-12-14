using System;
using System.Collections.Generic;
using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Aluno : IBaseEntity
    {
        public int Id { get; set; }
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }
    }
}