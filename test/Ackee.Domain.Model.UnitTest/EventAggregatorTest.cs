using Ackee.Domain.Model.EventManager;
using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using System;
using Xunit;

namespace Ackee.Domain.Model.UnitTest
{
    public class EventAggregatorTest
    {
        [Fact]
        public void subscribe_and_publish_an_event()
        {
            var eventSubscriber = new EventAggregator();

            bool subscribeCalled = false;

            Action<BookDoSomethingEvent> action = (bookDoSomethingEvent) =>
            {
                subscribeCalled = true;
            };
            eventSubscriber.Subscribe(action);


            eventSubscriber.Publish(new BookDoSomethingEvent());


            subscribeCalled.Should().BeTrue();
        }
    }
}