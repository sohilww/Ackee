using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;

namespace Ackee.DataAccess.LiteDB.IntegrationTest
{
    public class BookRepositoryFake : LiteDbRepository<Book, BookId>
    {
        public async override Task<BookId> GetNextId()
        {
            return new BookId(10);
        }

        public string ConnectionString => _connectionString;
    }
}