using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoMunicipioViewModel : IEquatable<ResultadoMunicipioViewModel>
    {
        public Disciplina Disciplina { get; set; }
        public Tema Tema { get; set; }
        public Descritor Descritor { get; set; }
        public int QtdQuestoesDisciplina { get; set; }
        public int QtdQuestoesTema { get; set; }
        public int QtdQuestoesDescritor { get; set; }
        public int QtdRespostasCorretas { get; set; }
        public int QtdQuestoes { get; set; }
        public int QtdAlunos { get; set; }
        public double TaxaAcerto => (double) QtdRespostasCorretas / (QtdQuestoes * QtdAlunos) * 100;

        public bool Equals(ResultadoMunicipioViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Disciplina, other.Disciplina) && Equals(Tema, other.Tema) && Equals(Descritor, other.Descritor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ResultadoMunicipioViewModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Disciplina, Tema, Descritor);
        }
    }
}