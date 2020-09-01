using Ackee.DataAccess.EventStore;
using Ackee.Domain.Model.TestUtility;

namespace Ackee.DataAccess.EventSource.IntegrationTest
{
    public class BookEventStoreTestRepository : EventStoreRepository<Book,BookId>
    {
        public string GetStreamName(BookId id)
        {
            return base.GetStreamName(id);
        }
    }
}