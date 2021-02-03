using SAED.Core.Interfaces;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Curso : IEntity
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Segmento> Segmentos { get; set; }
        public int Id { get; set; }
    }
}