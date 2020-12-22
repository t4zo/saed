using System.Collections.Generic;
using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Escola : IEntity
    {
        public int Id { get; set; }
        public int? Inep { get; set; }
        public int? MatrizId { get; set; }
        public Escola Matriz { get; set; }
        public string Nome { get; set; }
        public int DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int? Numero { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public ICollection<Sala> Salas { get; set; }
    }
}