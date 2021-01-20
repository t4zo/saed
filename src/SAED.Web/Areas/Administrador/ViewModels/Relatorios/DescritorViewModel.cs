using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class DescritorViewModel : IEquatable<DescritorViewModel>
    {
        public Descritor Descritor { get; set; }
        public Distrito Distrito { get; set; }
        public Escola Escola { get; set; }
        public Etapa Etapa { get; set; }
        public int QtdQuestoes { get; set; }
        public int QtdRespostasCorretas { get; set; }

        public bool Equals(DescritorViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Descritor, other.Descritor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DescritorViewModel) obj);
        }

        public override int GetHashCode()
        {
            return Descritor != null ? Descritor.GetHashCode() : 0;
        }
    }
}