using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class EscolaViewModel : IEquatable<EscolaViewModel>
    {
        public Escola Escola { get; set; }
        public Disciplina Disciplina { get; set; }
        public int QtdAlunos { get; set; }

        public bool Equals(EscolaViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Escola, other.Escola);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EscolaViewModel) obj);
        }

        public override int GetHashCode()
        {
            return Escola != null ? Escola.GetHashCode() : 0;
        }
    }
}