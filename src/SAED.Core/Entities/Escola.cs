using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Escola : IEntity, IEquatable<Escola>
    {
        public int? Inep { get; set; }
        public int? MatrizId { get; set; }
        public Escola Matriz { get; set; }
        public string Nome { get; set; }
        public int DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int? Numero { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public ICollection<Sala> Salas { get; set; }
        public int Id { get; set; }

        public bool Equals(Escola other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Inep == other.Inep && MatrizId == other.MatrizId && Nome == other.Nome && DistritoId == other.DistritoId && Bairro == other.Bairro && Rua == other.Rua &&
                   Numero == other.Numero && Email == other.Email && Telefone == other.Telefone;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Escola) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Inep);
            hashCode.Add(MatrizId);
            hashCode.Add(Nome);
            hashCode.Add(DistritoId);
            hashCode.Add(Bairro);
            hashCode.Add(Rua);
            hashCode.Add(Numero);
            hashCode.Add(Email);
            hashCode.Add(Telefone);
            return hashCode.ToHashCode();
        }
    }
}