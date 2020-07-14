using Ardalis.Specification;
using SAED.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;

namespace SAED.ApplicationCore.Specifications
{
    public class QuestoesWithSpecification : BaseSpecification<Questao>
    {
        public QuestoesWithSpecification() : base()
        {
            AddInclude(x => x.Descritor);
        }

        public QuestoesWithSpecification(Expression<Func<Questao, bool>> criteria) : base(criteria)
        {
            AddInclude(x => x.Descritor);
        }

    }
}
