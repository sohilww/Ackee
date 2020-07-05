using System.Collections.Generic;

namespace Ackee.Domain.Model
{
    public interface IUncommittedEvents
    {
        // void SetEvent(IDomainEvent @event);
        // void Clear();

        ICollection<IDomainEvent> UncommittedEvent { get; }
    }
}