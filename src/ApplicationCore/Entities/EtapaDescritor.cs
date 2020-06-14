namespace SAED.ApplicationCore.Entities
{
    public class EtapaDescritor
    {
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public int DescritorId { get; set; }
        public Descritor Descritor { get; set; }
    }
}