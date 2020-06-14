using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Turno : BaseEntity
    {
        public string Nome { get; set; }
        public string Regime { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}
