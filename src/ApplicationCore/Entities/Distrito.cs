using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Distrito : BaseEntity
    {
        public string Nome { get; set; }
        public ZonaEnum Zona { get; set; }
        public int Distancia { get; set; }
        public ICollection<Escola> Escolas { get; set; }
        public ICollection<AvaliacaoDistrito> AvaliacaoDistritos { get; set; }
    }

    public enum ZonaEnum
    {
        Urbana,
        Rural
    }
}
