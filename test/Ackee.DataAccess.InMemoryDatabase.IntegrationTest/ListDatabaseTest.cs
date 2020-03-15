using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Xunit;

namespace Ackee.DataAccess.ListDatabase.IntegrationTest
{
    public class ListDatabaseTest
    {
        [Fact]
        public async Task should_add_to_database()
        {
            var id = new IdFake(10);
            var aggregate = new AggregateRootFake(id);
            var database = new ListDatabase<AggregateRootFake, IdFake>();

            await database.Create(aggregate);

            var readAggregate = database.Context[id];

            aggregate.Should().Be(readAggregate);

        }
    }
}
