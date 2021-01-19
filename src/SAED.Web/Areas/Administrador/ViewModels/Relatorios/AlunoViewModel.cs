using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class AlunoViewModel : IEquatable<AlunoViewModel>
    {
        public Escola Escola { get; set; }
        public Etapa Etapa { get; set; }
        public Aluno Aluno { get; set; }
        public Disciplina Disciplina { get; set; }

        public bool Equals(AlunoViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Escola, other.Escola) && Equals(Etapa, other.Etapa) && Equals(Aluno, other.Aluno);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AlunoViewModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Escola, Etapa, Aluno);
        }
    }
}