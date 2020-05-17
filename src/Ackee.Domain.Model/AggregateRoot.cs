namespace Ackee.Domain.Model
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>,
        IAggregateRoot where TKey : Id
    {
        private IEventPublisher _eventPublisher;
        protected AggregateRoot(TKey id) : base(id)
        {
        }

        protected AggregateRoot() : base()
        {

        }


        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            _eventPublisher.Publish(@event);
        }
    }
}