using System;
using System.Collections.Generic;
using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Descritor : IBaseEntity, IEquatable<Descritor>
    {
        public int Id { get; set; }
        public int TemaId { get; set; }
        public Tema Tema { get; set; }
        public string Nome { get; set; }
        public ICollection<Questao> Questoes { get; set; }

        public bool Equals(Descritor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && TemaId == other.TemaId && Nome == other.Nome;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Descritor) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, TemaId, Nome);
        }
    }
}