using System;
using Ackee.Domain.Model;

namespace Ackee.Events
{
    public class EventData
    {

        public EventData()
        {
            Published = false;
        }
        public Guid Id { get; set; }
        public IDomainEvent DomainEvent { get; set; }
        public string Body { get; set; }
        public string BodyType { get; set; }
        public bool Published { get; }
        
    }
}
