﻿using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Questao : BaseEntity
    {
        public int DescritorId { get; set; }
        public Descritor Descritor { get; set; }
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Enunciado { get; set; }
        public bool Habilitada { get; set; }
        public ICollection<Alternativa> Alternativas { get; set; }
        public ICollection<AvaliacaoQuestao> AvaliacaoQuestoes { get; set; }
    }
}