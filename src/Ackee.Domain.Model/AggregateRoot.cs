using Ackee.Domain.Model.EventManager;
using System.Collections.Generic;

namespace Ackee.Domain.Model
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IUncommittedEvents,
        IAggregateRoot where TKey : Id
    {
        private IEventPublisher _eventPublisher;
        protected AggregateRoot(TKey id, IEventPublisher eventPublisher) : base(id)
        {
            UncommittedEvent = new List<IDomainEvent>();
            _eventPublisher = eventPublisher;
        }

        protected AggregateRoot() : base()
        {
            UncommittedEvent = new List<IDomainEvent>();
        }


        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            _eventPublisher.Publish(@event);
            UncommittedEvent.Add(@event);
        }

        public ICollection<IDomainEvent> UncommittedEvent { get; }

        public void SetPublisher(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }
    }
}