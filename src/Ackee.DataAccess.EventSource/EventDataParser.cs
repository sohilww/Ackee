using System.Collections.Generic;
using System.Text;
using Ackee.Domain.Model;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace Ackee.DataAccess.EventStore
{
    public class EventDataParser
    {
        public static EventData ConvertToJson(IDomainEvent domainEvent,byte[] metadata=null)
        {
            var type = domainEvent.GetType().AssemblyQualifiedName;
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(domainEvent));
            var eventData=new EventData(domainEvent.Id,type,true,data,metadata);
            return eventData;
        }
        public static List<EventData> ConvertToJson(IEnumerable<IDomainEvent> domainEvents)
        {
            List<EventData> data=new List<EventData>();
            
            foreach (var domainEvent in domainEvents)
            {
                data.Add(ConvertToJson(domainEvent));
            }
            return data;
        }
    }
}