namespace Ackee.Domain.Model.EventManager
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}