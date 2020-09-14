using System.Collections.Generic;

namespace Ackee.Domain.Model
{
    public interface IUncommittedEvents
    {
        ICollection<IDomainEvent> UncommittedEvent { get; }
    }
}