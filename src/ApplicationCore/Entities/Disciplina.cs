using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Disciplina : BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Tema> Temas { get; set; }
        public ICollection<AvaliacaoDisciplina> AvaliacaoDisciplinas { get; set; }
    }
}