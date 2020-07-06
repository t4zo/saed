using Ardalis.Specification;
using SAED.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;

namespace SAED.ApplicationCore.Specifications
{
    public class EscolasWithSpecification : BaseSpecification<Escola>
    {
        public EscolasWithSpecification() : base()
        {
            AddInclude(x => x.Distrito);
            AddInclude(x => x.Matriz);
        }

        public EscolasWithSpecification(Expression<Func<Escola, bool>> predicate) : base(predicate)
        {
            AddInclude(x => x.Distrito);
            AddInclude(x => x.Matriz);
        }
    }
}
