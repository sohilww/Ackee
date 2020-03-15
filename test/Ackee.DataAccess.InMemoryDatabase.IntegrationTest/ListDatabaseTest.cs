using System;
using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Xunit;

namespace Ackee.DataAccess.ListDatabase.IntegrationTest
{
    public class ListDatabaseTest : IDisposable
    {
        private ListDatabase<AggregateRootFake, IdFake> _database;
        private IdFake _id;
        private AggregateRootFake _aggregate;

        public ListDatabaseTest()
        {
            _database = new ListDatabase<AggregateRootFake, IdFake>();
            _id = new IdFake(10);
            _aggregate = new AggregateRootFake(_id);
        }

        [Fact]
        public async Task should_add_to_database()
        {
            await _database.Create(_aggregate);

            var readAggregate = await _database.Get(_id);
            _aggregate.Should().Be(readAggregate);
        }

        [Fact]
        public async Task Should_remove_aggregate_created_before()
        {
            await CreateAggregate();
            
            await _database.Remove(_aggregate);

            Func<Task> func= async ()=>await _database.Get(_id);
            func.Should().Throw<Exception>();
        }

        private async Task CreateAggregate()
        {
            await _database.Create(_aggregate);
        }

        public void Dispose()
        {
            _database.Clear();
        }
    }
}
