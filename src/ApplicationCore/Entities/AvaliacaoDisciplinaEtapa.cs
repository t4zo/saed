using SAED.ApplicationCore.Interfaces;

namespace SAED.ApplicationCore.Entities
{
    public class AvaliacaoDisciplinaEtapa : IManyToMany
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public int QtdQuestoes { get; set; }
    }
}
