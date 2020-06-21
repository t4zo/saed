using Ardalis.Specification;
using SAED.ApplicationCore.Entities;

namespace SAED.ApplicationCore.Specifications
{
    public sealed class AvaliacoesWithSpecification : BaseSpecification<Avaliacao>
    {
        public AvaliacoesWithSpecification(int id) : base(a => a.Id == id) { }
    }
}
