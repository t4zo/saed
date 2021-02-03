using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Tema : IEntity, IEquatable<Tema>
    {
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public string Nome { get; set; }
        public ICollection<Descritor> Descritores { get; set; }
        public int Id { get; set; }


        public bool Equals(Tema other)
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
            return Equals((Tema) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}