using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;

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
        ARealizar = 0,
        EmAndamento = 1,
        Finalizada = 2,
    }
}