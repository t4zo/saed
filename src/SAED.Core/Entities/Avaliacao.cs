using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SAED.Core.Interfaces;
using System;

namespace SAED.Core.Entities
{
    public class Avaliacao : IEntity, IEquatable<Avaliacao>
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = "";
        public StatusAvaliacao Status { get; set; }
        public ICollection<Questao> Questoes { get; set; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }

        public bool Equals(Avaliacao other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Avaliacao) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    public enum StatusAvaliacao
    {
        [Display(Name = "A Realizar")] ARealizar,

        [Display(Name = "Em Andamento")] EmAndamento,

        [Display(Name = "Finalizada")] Finalizada
    }
}