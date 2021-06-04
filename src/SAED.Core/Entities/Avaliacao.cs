using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAED.Core.Entities
{
    public class Avaliacao : IEntity, IEquatable<Avaliacao>
    {
        public string Codigo { get; set; } = "";
        public StatusAvaliacao Status { get; set; }
        public ICollection<Questao> Questoes { get; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }
        public int Id { get; set; }
        
        private Avaliacao()
        {
            Questoes = new List<Questao>();
            AvaliacaoDisciplinasEtapas = new List<AvaliacaoDisciplinaEtapa>();
            RespostaAlunos = new List<RespostaAluno>();
        }

        public Avaliacao(int id, string codigo, StatusAvaliacao status) : this()
        {
            Id = id;
            Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
            Status = status;
        }


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
            if (obj.GetType() != GetType()) return false;
            return Equals((Avaliacao) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    public enum StatusAvaliacao
    {
        [Display(Name = "A Realizar")]
        ARealizar,

        [Display(Name = "Em Andamento")]
        EmAndamento,

        [Display(Name = "Finalizada")]
        Finalizada
    }
}