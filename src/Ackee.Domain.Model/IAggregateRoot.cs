﻿namespace Ackee.Domain.Model
{
    public interface IAggregateRoot
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}
