using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Questao : BaseEntity
    {
        public int DescritorId { get; set; }
        public Descritor Descritor { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string HTML { get; set; }
        public bool Habilitada { get; set; }
        public ICollection<Alternativa> Alternativas { get; set; }
        public ICollection<QuestaoAvaliacao> QuestaoAvaliacoes { get; set; }
    }
}