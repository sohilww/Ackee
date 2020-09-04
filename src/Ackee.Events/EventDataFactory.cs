using System.Collections.Generic;
using System.Linq;
using Ackee.Domain.Model;
using Newtonsoft.Json;

namespace Ackee.Events
{
    public static class EventDataFactory
    {
        public static EventData Create(IDomainEvent uncommittedEvent)
        {
            var eventData=new EventData
            {
                BodyType = uncommittedEvent.GetType().ToString(),
                Body = JsonConvert.SerializeObject(uncommittedEvent),
                DomainEvent = uncommittedEvent,
                Id = uncommittedEvent.Id,
            };

            return eventData;
        }
        public static List<EventData> Create(IEnumerable<IDomainEvent> uncommittedEvents)
        {
            return uncommittedEvents.Select(Create).ToList();
        }
    }
}