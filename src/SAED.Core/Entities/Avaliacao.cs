using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAED.Core.Entities
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
        [Display(Name = "A Realizar")] ARealizar,

        [Display(Name = "Em Andamento")] EmAndamento,

        [Display(Name = "Finalizada")] Finalizada
    }
}