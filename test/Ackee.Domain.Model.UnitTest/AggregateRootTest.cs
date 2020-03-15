using System;
using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class AggregateRootTest
    {
        [Fact]
        public void should_create_entity_from_IAggregateRoot()
        {
            var aggregateRoot=new AggregateRootFake();

            aggregateRoot.Should().BeAssignableTo<IAggregateRoot>();
        }
        
    }
}
