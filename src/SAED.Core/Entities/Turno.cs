using System.Collections.Generic;
using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Turno : IBaseEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}