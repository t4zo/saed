using SAED.Core.Entities;
using System;

namespace SAED.Web.Areas.Administrador.ViewModels.Relatorios
{
    public class ResultadoMunicipioViewModel : IEquatable<ResultadoMunicipioViewModel>
    {
        public Disciplina Disciplina { get; set; }
        public int DistritoId { get; set; }
        public int EtapaId { get; set; }
        public int QtdQuestoesDisciplina { get; set; }
        public int QtdRespostasCorretas { get; set; }
        public int QtdQuestoes { get; set; }
        public int QtdAlunos { get; set; }
        public double TaxaAcerto => (double) QtdRespostasCorretas / (QtdQuestoes * QtdAlunos) * 100;

        

        public bool Equals(ResultadoMunicipioViewModel other)
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
            return Equals((ResultadoMunicipioViewModel) obj);
        }

        public override int GetHashCode()
        {
            return Disciplina != null ? Disciplina.GetHashCode() : 0;
        }
    }
}