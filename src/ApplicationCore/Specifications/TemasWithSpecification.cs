using Ardalis.Specification;
using SAED.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;

namespace SAED.ApplicationCore.Specifications
{
    public class TemasWithSpecification : BaseSpecification<Tema>
    {
        public TemasWithSpecification() : base()
        {
            AddInclude(x => x.Disciplina);
        }

        public TemasWithSpecification(Expression<Func<Tema, bool>> criteria) : base(criteria)
        {
            AddInclude(x => x.Disciplina);
        }
    }
}
