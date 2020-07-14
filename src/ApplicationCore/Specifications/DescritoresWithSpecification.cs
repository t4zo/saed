using Ardalis.Specification;
using SAED.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;

namespace SAED.ApplicationCore.Specifications
{
    public class DescritoresWithSpecification : BaseSpecification<Descritor>
    {
        public DescritoresWithSpecification() : base()
        {
            AddInclude(x => x.Tema.Disciplina);
            AddInclude(x => x.Tema);
        }

        public DescritoresWithSpecification(Expression<Func<Descritor, bool>> criteria) : base(criteria)
        {
            AddInclude(x => x.Tema.Disciplina);
            AddInclude(x => x.Tema);
        }

    }
}
