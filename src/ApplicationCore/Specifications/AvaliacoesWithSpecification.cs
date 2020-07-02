using Ardalis.Specification;
using SAED.ApplicationCore.Entities;

namespace SAED.ApplicationCore.Specifications
{
    public sealed class AvaliacoesWithSpecification : BaseSpecification<Avaliacao>
    {
        public AvaliacoesWithSpecification(int id) : base(x => x.Id == id) { }
    }
}
