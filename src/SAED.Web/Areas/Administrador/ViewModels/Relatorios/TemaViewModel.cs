using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class TemaViewModel : IEquatable<TemaViewModel>
    {
        public Tema Tema { get; set; }
        public Distrito Distrito { get; set; }
        public Escola Escola { get; set; }
        public Etapa Etapa { get; set; }
        public int QtdQuestoesTema { get; set; }
        public int QtdRespostasCorretas { get; set; }

        public bool Equals(TemaViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Tema, other.Tema);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TemaViewModel) obj);
        }

        public override int GetHashCode()
        {
            return Tema != null ? Tema.GetHashCode() : 0;
        }
    }
}