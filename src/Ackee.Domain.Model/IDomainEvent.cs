using System;

namespace Ackee.Domain.Model
{
    //Todo: it can change to internal
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime PublishDateTime { get; }
    }
}