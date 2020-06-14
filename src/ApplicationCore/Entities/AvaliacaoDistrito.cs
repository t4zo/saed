namespace SAED.ApplicationCore.Entities
{
    public class AvaliacaoDistrito
    {
        public int AvaliacaoId { get; set; }
        public Avaliacao Avaliacao { get; set; }
        public int DistritoId { get; set; }
        public Distrito Distrito { get; set; }
    }
}