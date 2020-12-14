using System.Collections.Generic;
using SAED.Core.Interfaces;

namespace SAED.Core.Entities
{
    public class Segmento : IBaseEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public ICollection<Etapa> Etapas { get; set; }
    }
}