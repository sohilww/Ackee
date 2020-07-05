using System.Net.Http.Headers;

namespace Ackee.Specification.Test
{
    public class FalseSpecificationTest:CompositeSpecification<object>
    {
        public override bool IsSatisfiedBy(object candidate)
        {
            return false;
        }
    }
}