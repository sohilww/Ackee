using Ackee.Config;
using EventStore.ClientAPI;

namespace Ackee.DataAccess.EventSource
{
    public class EventSourceModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            var connection = EventStoreConnection.Create("127.0.0.1:2113");
            connection.ConnectAsync().Wait();


        }
    }
}