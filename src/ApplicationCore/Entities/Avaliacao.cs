using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAED.ApplicationCore.Entities
{
    public class Avaliacao : BaseEntity
    {
        public string Codigo { get; set; } = "";
        public StatusAvaliacao Status { get; set; }
        public ICollection<UsuarioTurmaAvaliacao> UsuarioTurmaAvaliacao { get; set; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }
        public ICollection<AvaliacaoQuestao> AvaliacaoQuestoes { get; set; }
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