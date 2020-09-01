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
         

            _repository = new BookEventStoreTestRepository();
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

            streamName.Should().Be("Book-10");
        }

        [Fact]
        public async Task Get_aggregate()
        {
            var settings = ConnectionSettings
                .Create()
                .DisableServerCertificateValidation()
                .UseDebugLogger()
                .Build();

            _connection = EventStoreConnection
                .Create(settings,new Uri("tcp://admin:changeit@127.0.0.1:1113"));

            var book = BookFactoryTest.Create();
            book.DoSomethingAndPublishEvent();
            var streamName = _repository.GetStreamName(book.Id);
            
            
            var eventData = EventDataParser.ConvertToJson(book.UncommittedEvent.AsEnumerable());
            await _connection.ConnectAsync();
            await _connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventData);

            var result= await _connection
                .ReadStreamEventsForwardAsync(streamName,0,1,false);
        }
    }
}