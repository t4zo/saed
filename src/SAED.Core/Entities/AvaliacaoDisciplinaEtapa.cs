using SAED.Core.Interfaces;
using System;

namespace SAED.Core.Entities
{
    public class AvaliacaoDisciplinaEtapa : IManyToMany, IEquatable<AvaliacaoDisciplinaEtapa>
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }

        public bool Equals(AvaliacaoDisciplinaEtapa other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AvaliacaoId == other.AvaliacaoId && DisciplinaId == other.DisciplinaId && EtapaId == other.EtapaId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AvaliacaoDisciplinaEtapa) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AvaliacaoId, DisciplinaId, EtapaId);
        }
    }
}