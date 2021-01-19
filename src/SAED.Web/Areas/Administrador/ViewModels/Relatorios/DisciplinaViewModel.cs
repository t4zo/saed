using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class DisciplinaViewModel : IEquatable<DisciplinaViewModel>
    {
        public Disciplina Disciplina { get; set; }
        public Distrito Distrito { get; set; }
        public Escola Escola { get; set; }
        public Etapa Etapa { get; set; }
        public Aluno Aluno { get; set; }
        public int QtdQuestoesDisciplina { get; set; }
        public int QtdRespostasCorretas { get; set; }

        public bool Equals(DisciplinaViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Disciplina, other.Disciplina);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DisciplinaViewModel) obj);
        }

        public override int GetHashCode()
        {
            return Disciplina != null ? Disciplina.GetHashCode() : 0;
        }
    }
}