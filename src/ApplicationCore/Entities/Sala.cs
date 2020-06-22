using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Sala : BaseEntity
    {
        public int Numero { get; set; }
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}
