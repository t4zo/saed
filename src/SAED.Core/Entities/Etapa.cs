﻿using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Etapa : BaseEntity
    {
        public string Nome { get; set; }
        public int SegmentoId { get; set; }
        public Segmento Segmento { get; set; }
        public int Normativa { get; set; }
        public ICollection<Turma> Turmas { get; set; }
        public ICollection<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }
        public ICollection<Questao> Questoes { get; set; }
    }
}