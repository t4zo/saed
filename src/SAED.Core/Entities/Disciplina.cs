using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Disciplina : BaseEntity
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Tema> Temas { get; set; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }
    }
}