using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Etapa : IEntity, IEquatable<Etapa>
    {
        public string Nome { get; set; }
        public int SegmentoId { get; set; }
        public Segmento Segmento { get; set; }
        public int Normativa { get; set; }
        public ICollection<Turma> Turmas { get; set; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }
        public ICollection<Questao> Questoes { get; set; }
        public int Id { get; set; }

        public bool Equals(Etapa other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public void ClearReferenceCycle()
        {
            Turmas = null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Etapa) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}