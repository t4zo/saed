﻿using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Turno : BaseEntity
    {
        public string Nome { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}