using System;
using System.Collections.Generic;
using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Disciplina : IBaseEntity, IEquatable<Disciplina>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Tema> Temas { get; set; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }

        public bool Equals(Disciplina other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Nome == other.Nome && Sigla == other.Sigla && Equals(Temas, other.Temas) &&
                   Equals(AvaliacaoDisciplinasEtapas, other.AvaliacaoDisciplinasEtapas);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Disciplina) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nome, Sigla, Temas, AvaliacaoDisciplinasEtapas);
        }
    }
}