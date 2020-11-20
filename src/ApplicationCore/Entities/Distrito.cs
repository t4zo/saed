using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Distrito : BaseEntity
    {
        public string Nome { get; set; }
        public Zona Zona { get; set; }
        public int Distancia { get; set; }
        public ICollection<Escola> Escolas { get; set; }
    }

    public enum Zona
    {
        Urbana,
        Rural
    }
}