using SAED.Core.Interfaces;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Sala : IEntity
    {
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public double Area { get; set; }
        public ICollection<Turma> Turmas { get; set; }
        public int Id { get; set; }
    }
}