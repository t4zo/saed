using SAED.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SAED.Core.Entities
{
    public class Distrito : IEntity, IEquatable<Distrito>
    {
        public string Nome { get; set; }
        public Zona Zona { get; set; }
        public int Distancia { get; set; }
        public ICollection<Escola> Escolas { get; }
        public int Id { get; set; }
        
        private Distrito()
        {
            Escolas = new List<Escola>();
        }

        public Distrito(int id, string nome, Zona zona, int distancia) : this(id, nome, zona, distancia, new List<Escola>())
        {
        }
        
        public Distrito(int id, string nome, Zona zona, int distancia, ICollection<Escola> escolas)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Zona = zona;
            Distancia = distancia;

            if (escolas.Any(x => x.DistritoId != id))
            {
                throw new DataException("Escola com distritoId inválido");
            }
            
            Escolas = escolas;
        }


        public bool Equals(Distrito other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Distrito) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    public enum Zona
    {
        Urbana,
        Rural
    }
}