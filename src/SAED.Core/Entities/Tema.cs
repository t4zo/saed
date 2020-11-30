using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Tema : BaseEntity
    {
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public string Nome { get; set; }
        public ICollection<Descritor> Descritores { get; set; }
    }
}