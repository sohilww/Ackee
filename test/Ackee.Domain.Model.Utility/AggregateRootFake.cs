using System;

namespace Ackee.Domain.Model.TestUtility
{
    public class AggregateRootFake : AggregateRoot<IdFake>
    {
        public AggregateRootFake(IdFake id) : base(id)
        {
        }

        public void DoSomethingAndPublishEvent()
        {
            Publish(new DoSomethingEvent(Id));
        }
    }
}
