namespace Ackee.Specification
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> other;
        public NotSpecification(ISpecification<T> other) => this.other = other;
        public override bool IsSatisfiedBy(T candidate) => !other.IsSatisfiedBy(candidate);
    }
}