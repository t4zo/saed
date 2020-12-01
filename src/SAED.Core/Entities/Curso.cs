﻿using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Curso : BaseEntity
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Segmento> Segmentos { get; set; }
    }
}