using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Sala : BaseEntity
    {
        public string Nome { get; set; }
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}
