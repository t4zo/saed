using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Descritor : BaseEntity, IAggregateRoot
    {
        public int TemaId { get; set; }
        public Tema Tema { get; set; }
        public string Nome { get; set; }
        public ICollection<Questao> Questoes { get; set; }
        public ICollection<EtapaDescritor> EtapaDescritores { get; set; }
    }
}