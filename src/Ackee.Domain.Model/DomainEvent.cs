using System;

namespace Ackee.Domain.Model
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; protected set; }
        public DateTime OccurredOn { get; protected set; }
        protected DomainEvent()
        {
            this.Id = Guid.NewGuid();
            this.OccurredOn = DateTime.Now;
        }
    }
}