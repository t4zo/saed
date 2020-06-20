using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
   public class Escola : BaseEntity, IAggregateRoot
    {
        public int? Inep { get; set; }
        public int? MatrizId { get; set; }
        public Escola Matriz { get; set; }
        public string Nome { get; set; }
        public int DistritoId { get; set; }
        public Distrito Distrito { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public ICollection<Sala> Salas { get; set; }
    }
}
