using System;

namespace Ackee.Domain.Model.EventManager
{
    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IDomainEvent;
    }
}