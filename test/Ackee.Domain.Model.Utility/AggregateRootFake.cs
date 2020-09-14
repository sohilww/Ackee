namespace Ackee.Domain.Model.TestUtility
{
    public class AggregateRootFake : AggregateRoot<IdFake>
    {
        public AggregateRootFake(IdFake id) : base(id, new EventPublisherFake())
        {
        }

        public void DoSomethingAndPublishEvent()
        {
            Publish(new DoSomethingEvent(Id));
        }
    }
}
