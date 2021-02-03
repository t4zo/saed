using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class DistritoViewModel : IEquatable<DistritoViewModel>
    {
        public Distrito Distrito { get; set; }
        public Disciplina Disciplina { get; set; }
        public Tema Tema { get; set; }
        public Descritor Descritor { get; set; }
        public int QtdAlunos { get; set; }

        public bool Equals(DistritoViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Distrito, other.Distrito);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DistritoViewModel) obj);
        }

        public override int GetHashCode()
        {
            return Distrito is not null ? Distrito.GetHashCode() : 0;
        }
    }
}