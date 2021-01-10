using SAED.Core.Interfaces;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Alternativa : IEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Correta { get; set; }
        public int QuestaoId { get; set; }
        public Questao Questao { get; set; }
        public ICollection<RespostaAluno> RespostaAlunos { get; set; }

        public void ClearReferenceCycle()
        {
            Questao = null;
        }
    }
}