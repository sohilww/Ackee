namespace Ackee.Domain.Model.TestUtility
{
    public class AggregateRootFake : AggregateRoot<IdFake>
    {
        public AggregateRootFake(IdFake id) : base(id)
        {
        }
    }
}
