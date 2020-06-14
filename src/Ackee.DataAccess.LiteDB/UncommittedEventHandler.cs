using System.Collections.Generic;
using Ackee.Domain.Model;
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
                _db.Insert(uncommittedEvent, "Events");
            }
            
        }
    }
}