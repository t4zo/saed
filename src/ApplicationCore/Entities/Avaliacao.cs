using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAED.ApplicationCore.Entities
{
    public class Avaliacao : BaseEntity, IAggregateRoot
    {
        public string Codigo { get; set; } = "";
        public StatusAvaliacao Status { get; set; }
        public ICollection<UsuarioTurmaAvaliacao> UsuarioTurmaAvaliacao { get; set; }
        public ICollection<AvaliacaoDisciplina> AvaliacaoDisciplinas { get; set; }
        public ICollection<AvaliacaoDistrito> AvaliacaoDistritos { get; set; }
        public ICollection<QuestaoAvaliacao> QuestaoAvaliacoes { get; set; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }
    }

    public enum StatusAvaliacao
    {
        [Display(Name = "A Realizar")]
        ARealizar = 0,

        [Display(Name = "Em Andamento")]
        EmAndamento = 1,

        [Display(Name = "Finalizada")]
        Finalizada = 2,
    }
}