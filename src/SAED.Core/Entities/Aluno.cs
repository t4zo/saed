using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Aluno : IEntity, IEquatable<Aluno>
    {
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public DateTime Nascimento { get; set; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }
        public int Id { get; set; }

        public bool Equals(Aluno other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Aluno) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}