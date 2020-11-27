﻿using SAED.ApplicationCore.Interfaces;

namespace SAED.ApplicationCore.Entities
{
    public class UsuarioTurmaAvaliacao : IManyToMany
    {
        public int ApplicationUserId { get; set; }
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
    }
}