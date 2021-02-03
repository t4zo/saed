using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Descritor : IEntity, IEquatable<Descritor>
    {
        public int TemaId { get; set; }
        public Tema Tema { get; set; }
        public string Nome { get; set; }
        public ICollection<Questao> Questoes { get; set; }
        public int Id { get; set; }


        public bool Equals(Descritor other)
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
            return Equals((Descritor) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}