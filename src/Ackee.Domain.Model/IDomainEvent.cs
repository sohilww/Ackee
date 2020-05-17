using System;

namespace Ackee.Domain.Model
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime PublishDateTime { get; }
    }
}