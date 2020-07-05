using System.Collections.Generic;

namespace Ackee.Domain.Model
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IUncommittedEvents,
        IAggregateRoot where TKey : Id
    {
        private IEventPublisher _eventPublisher;
        protected AggregateRoot(TKey id) : base(id)
        {
            UncommittedEvent=new List<IDomainEvent>();
        }

        protected AggregateRoot() : base()
        {
            UncommittedEvent = new List<IDomainEvent>();
        }


        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            //_eventPublisher.Publish(@event);
            UncommittedEvent.Add(@event);
        }

        public ICollection<IDomainEvent> UncommittedEvent { get; }
    }
}