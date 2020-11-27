using System.Collections.Generic;

namespace SAED.ApplicationCore.Entities
{
    public class Segmento : BaseEntity
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public ICollection<Etapa> Etapas { get; set; }
    }
}