using SAED.Core.Interfaces;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Turno : IEntity
    {
        public string Nome { get; set; }
        public ICollection<Turma> Turmas { get; set; }
        public int Id { get; set; }
    }
}