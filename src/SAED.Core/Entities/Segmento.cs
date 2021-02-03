using SAED.Core.Interfaces;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Segmento : IEntity
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public ICollection<Etapa> Etapas { get; set; }
        public int Id { get; set; }
    }
}