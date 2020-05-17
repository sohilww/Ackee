namespace Ackee.Domain.Model
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}