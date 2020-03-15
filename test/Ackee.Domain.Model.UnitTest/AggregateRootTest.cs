using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class AggregateRootTest
    {
        [Fact]
        public void Should_create_entity_from_IAggregateRoot()
        {
            var id=new IdFake(10);
            var aggregateRoot=new AggregateRootFake(id);

            aggregateRoot.Should().BeAssignableTo<IAggregateRoot>();
        }
        
    }
}
