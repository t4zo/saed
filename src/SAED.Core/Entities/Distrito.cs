﻿using System;
using System.Collections.Generic;
using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Distrito : IEntity, IEquatable<Distrito>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Zona Zona { get; set; }
        public int Distancia { get; set; }
        public ICollection<Escola> Escolas { get; set; }

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
            if (obj.GetType() != this.GetType()) return false;
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