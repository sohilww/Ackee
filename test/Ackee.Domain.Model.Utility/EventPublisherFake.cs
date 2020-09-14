using Ackee.Domain.Model.EventManager;

namespace Ackee.Domain.Model.TestUtility
{
    public class EventPublisherFake : IEventPublisher
    {
        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {

        }
    }
}