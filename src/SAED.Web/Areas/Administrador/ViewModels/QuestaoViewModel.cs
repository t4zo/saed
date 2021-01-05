using SAED.Core.Entities;
using SAED.Core.Interfaces;
using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels
{
    public class QuestaoViewModel : IEntity
    {
        public int DescritorId { get; set; }
        public Descritor Descritor { get; set; }
        public int EtapaId { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Enunciado { get; set; }
        public bool Habilitada { get; set; }
        public IList<Alternativa> Alternativas { get; set; }
        public int Id { get; set; }
    }
}