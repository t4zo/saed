using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Questao : BaseEntity, IAggregateRoot
    {
        public int DescritorId { get; set; }
        public Descritor Descritor { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Html { get; set; }
        public bool Habilitada { get; set; }
        public ICollection<Alternativa> Alternativas { get; set; }
        public ICollection<QuestaoAvaliacao> QuestaoAvaliacoes { get; set; }
    }
}