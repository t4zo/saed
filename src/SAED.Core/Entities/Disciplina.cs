using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Disciplina : IEntity, IEquatable<Disciplina>
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Tema> Temas { get; set; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }
        public int Id { get; set; }


        public bool Equals(Disciplina other)
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
            return Equals((Disciplina) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}