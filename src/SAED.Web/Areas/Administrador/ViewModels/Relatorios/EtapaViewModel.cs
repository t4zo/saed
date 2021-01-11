using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class EtapaViewModel : IEquatable<EtapaViewModel>
    {
        public Etapa Etapa { get; set; }
        public int DisciplinaId { get; set; }
        public int QtdAlunos { get; set; }

        public bool Equals(EtapaViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Etapa, other.Etapa);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EtapaViewModel) obj);
        }

        public override int GetHashCode()
        {
            return (Etapa != null ? Etapa.GetHashCode() : 0);
        }
    }
}