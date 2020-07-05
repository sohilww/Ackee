using System.Collections.Generic;
using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;
using Ackee.Events;
using FluentAssertions;
using Xunit;

namespace Ackee.DataAccess.LiteDB.IntegrationTest
{
    public class UncommittedEventHandlerTest : LiteDbBaseClassTest
    {
        private readonly BookRepositoryFake _bookRepository;
        private readonly Book _aggregate;
        private readonly string _eventsCollectionName;

        public UncommittedEventHandlerTest()
        {
            _bookRepository = new BookRepositoryFake(Db);
            _aggregate = BookFactoryTest.Create();
            _eventsCollectionName = "Events";
        }
        [Fact]
        public async Task If_aggregate_has_events_it_saves_on_create_method_of_aggregate_in_UncommittedEvent_collection()
        {
            _aggregate.DoSomethingAndPublishEvent();

            await _bookRepository.Create(_aggregate);

            var events = GetEvents();

            events.Should().NotBeEmpty();
        }

        [Fact]
        public async Task If_aggregate_has_event_it_saves_on_update_method_of_aggregate_in_unCommittedEvent_collection()
        {
            await _bookRepository.Create(_aggregate);
            
            _aggregate.DoSomethingAndPublishEvent();
            await _bookRepository.Update(_aggregate);
            
            var events = GetEvents();

            events.Should().NotBeEmpty();
        }

        [Fact]
        public async Task If_aggregate_has_event_it_saves_on_remove_method_of_aggregate_in_unCommittedEvent_collection()
        {
            await _bookRepository.Create(_aggregate);

            
            _aggregate.DoSomethingAndPublishEvent();
            await _bookRepository.Remove(_aggregate);

            var events = GetEvents();

            events.Should().NotBeEmpty();
        }

        private List<EventData> GetEvents()
        {
            return Db.Query<EventData>(_eventsCollectionName).ToList();
        }
    }
}