using System;
using System.Linq;
using System.Threading.Tasks;
using Ackee.DataAccess.EventStore;
using Ackee.Domain.Model.TestUtility;
using EventStore.ClientAPI;
using FluentAssertions;
using Xunit;

namespace Ackee.DataAccess.EventSource.IntegrationTest
{
    public class EventStoreRepositoryTest
    {
        private readonly BookEventStoreTestRepository _repository;
        private IEventStoreConnection _connection;

        public EventStoreRepositoryTest()
        {
            _connection = EventStoreConnection.Create(new Uri("tcp://127.0.0.1:2113"));
            
            _repository=new BookEventStoreTestRepository();
            
        }
        // [Fact]
        // public void Create_Book_Aggregate()
        // {
        //     var connection = EventStoreConnection.Create("");
        //     var repository = new EventStoreRepostiory(connection);
        // }

        [Fact]
        public async Task GetStreamName()
        {
            var bookId = new BookId(10);
            var streamName = _repository.GetStreamName(bookId);

            streamName.Should().Contain(bookId.ToString());
        }

        [Fact]
        public async Task Get_aggregate()
        {
            var book=BookFactoryTest.Create();
            var eventData= EventDataParser.ConvertToJson(book.UncommittedEvent.AsEnumerable());
            _connection.ConnectAsync().Wait();
            await _connection.AppendToStreamAsync(_repository.GetStreamName(book.Id), ExpectedVersion.Any, eventData);


            
        }
    }
}
