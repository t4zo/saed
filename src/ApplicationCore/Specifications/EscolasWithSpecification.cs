using Ardalis.Specification;
using SAED.ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    public class EscolasWithSpecification : BaseSpecification<Escola>
    {
        public EscolasWithSpecification() : base()
        {
            AddInclude(x => x.Distrito);
        }
    }
}
