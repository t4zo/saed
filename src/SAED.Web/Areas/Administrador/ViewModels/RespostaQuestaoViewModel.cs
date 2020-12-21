using SAED.Core.Entities;
using System.Collections.Generic;

namespace SAED.Web.Areas.Administrador.ViewModels
{
    public class RespostaQuestaoViewModel
    {
        //public int Id { get; set; }
        //public int DescritorId { get; set; }
        //public Descritor Descritor { get; set; }
        //public int EtapaId { get; set; }
        //public Etapa Etapa { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string Enunciado { get; set; }
        public IList<Alternativa> Alternativas { get; set; }

        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }
        public int AlternativaId { get; set; }
        public Alternativa Alternativa { get; set; }
    }
}