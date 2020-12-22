﻿using System.Collections.Generic;
using SAED.Core.Interfaces;
using System;

namespace SAED.Core.Entities
{
    public class Questao : IBaseEntity, IEquatable<Questao>
    {
        public int Id { get; set; }
        public int DescritorId { get; set; }
        public Descritor Descritor { get; set; }
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Enunciado { get; set; }
        public bool Habilitada { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
        public ICollection<Alternativa> Alternativas { get; set; }

        public bool Equals(Questao other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && DescritorId == other.DescritorId && EtapaId == other.EtapaId && Item == other.Item && Descricao == other.Descricao && Enunciado == other.Enunciado && Habilitada == other.Habilitada;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Questao) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DescritorId, EtapaId, Item, Descricao, Enunciado, Habilitada);
        }
    }
}