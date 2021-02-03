using SAED.Core.Interfaces;
using System.Collections.Generic;

namespace SAED.Core.Entities
{
    public class Turma : IEntity
    {
        public int SalaId { get; set; }
        public Sala Sala { get; set; }
        public int EtapaId { get; set; }
        public Etapa Etapa { get; set; }
        public int TurnoId { get; set; }
        public Turno Turno { get; set; }
        public int FormaId { get; set; }
        public Forma Forma { get; set; }
        public string Nome { get; set; }
        public bool Extinta { get; set; }
        public ICollection<Aluno> Alunos { get; set; }
        public int Id { get; set; }

        public void ClearReferenceCycle()
        {
            Sala = null;
            Etapa = null;
            Turno = null;
            Forma = null;
        }
    }
}