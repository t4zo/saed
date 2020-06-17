using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Distrito : BaseEntity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Zona { get; set; }
        public int Distancia { get; set; }
        public ICollection<Escola> Escolas { get; set; }
        public ICollection<AvaliacaoDistrito> AvaliacaoDistritos { get; set; }
    }
}
