using System.Collections.Generic;
using SAED.Core.Interfaces;
using System;

namespace SAED.Core.Entities
{
    public class Questao : IEntity, IEquatable<Questao>
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
        public IList<Avaliacao> Avaliacoes { get; set; }
        public IList<Alternativa> Alternativas { get; set; }


        public void ClearReferenceCycle()
        {
            Descritor.Questoes = null;
            Descritor.Tema.Descritores = null;
            Descritor.Tema.Disciplina.Temas = null;
        }
        
        public bool Equals(Questao other)
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
            return Equals((Questao) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}