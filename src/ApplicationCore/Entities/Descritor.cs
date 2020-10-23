﻿using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Descritor : BaseEntity
    {
        public int TemaId { get; set; }
        public Tema Tema { get; set; }
        public string Nome { get; set; }
        public ICollection<Questao> Questoes { get; set; }
        public ICollection<EtapaDescritor> EtapaDescritores { get; set; }
    }
}