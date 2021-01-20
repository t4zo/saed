using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class EscolaEtapaViewModel : IEquatable<EscolaEtapaViewModel>
    {
        public Escola Escola { get; set; }
        public Etapa Etapa { get; set; }
        public Disciplina Disciplina { get; set; }
        public Tema Tema { get; set; }
        public Descritor Descritor { get; set; }
        public int QtdAlunos { get; set; }

        public bool Equals(EscolaEtapaViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Escola, other.Escola) && Equals(Etapa, other.Etapa);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EscolaEtapaViewModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Escola, Etapa);
        }
    }
}