using System.Collections.Generic;
using Ackee.Domain.Model;
using Ackee.Events;
using LiteDB;

namespace Ackee.DataAccess.LiteDB
{
    public class UncommittedEventHandler
    {
        private readonly LiteRepository _db;
        public UncommittedEventHandler(LiteRepository db)
        {
            _db = db;
        }
        public void Handle(ICollection<IDomainEvent> uncommittedEvents)
        {
            foreach (var uncommittedEvent in uncommittedEvents)
            {
                var @event= EventDataFactory.Create(uncommittedEvent);
                _db.Insert(@event, "Events");
            }
            
        }
    }
}