using System;
using System.Collections.Generic;
using System.Linq;

namespace Ackee.Domain.Model.EventManager
{
    public class EventAggregator : IEventPublisher, IEventSubscriber
    {
        private readonly List<object> _subscriberList = new List<object>();
        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            foreach (var action in _subscriberList.OfType<Action<TEvent>>())
            {
                action.Invoke(@event);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IDomainEvent
        {
            _subscriberList.Add(action);
        }
    }
}