using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class AggregateRootTest
    {
        [Fact]
        public void Create_entity_from_IAggregateRoot()
        {
            var id=new IdFake(10);
            var aggregateRoot=new AggregateRootFake(id);

            aggregateRoot.Should().BeAssignableTo<IAggregateRoot>();
        }

        [Fact]
        public void Published_events_save_in_uncommitted_Collection()
        {
            var aggregate = CreateAggregate();

            aggregate.DoSomethingAndPublishEvent();

            aggregate.UncommittedEvent.Should().NotBeEmpty();

        }

        private AggregateRootFake CreateAggregate()
        {
            return new AggregateRootFake(new IdFake(20));
        }
    }
}
