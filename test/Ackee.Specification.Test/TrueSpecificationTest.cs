namespace Ackee.Specification.Test
{
    public class TrueSpecificationTest: CompositeSpecification<object>
    {
        public override bool IsSatisfiedBy(object candidate)
        {
            return true;
        }
    }
}