using System;
using System.Linq.Expressions;

namespace Ackee.Specification.Test
{
    public class TrueLinqSpecificationTest:LinqSpecification<object>
    {
        public override Expression<Func<object, bool>> AsExpression() => o => true;

    }
}