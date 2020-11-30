﻿using SAED.Core.Entities;

namespace SAED.Web.Areas.Aplicador.ViewModels
{
    public class ChooseAlunoRequest
    {
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
    }
}