using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Tema : BaseEntity, IAggregateRoot
    {
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public string Nome { get; set; }
        public ICollection<Descritor> Descritores { get; set; }
    }
}