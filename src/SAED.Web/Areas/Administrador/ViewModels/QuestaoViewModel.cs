using System.Collections.Generic;
using SAED.Core.Entities;
using SAED.Core.Interfaces;

namespace SAED.Web.Areas.Administrador.ViewModels
{
    public class QuestaoViewModel : IBaseEntity
    {
        public int Id { get; set; }
        public int DescritorId { get; set; }
        public Descritor Descritor { get; set; }
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Enunciado { get; set; }
        public bool Habilitada { get; set; }
        public IList<Alternativa> Alternativas { get; set; }
        public ICollection<AvaliacaoQuestao> AvaliacaoQuestoes { get; set; }
    }
}