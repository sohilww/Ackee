using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;

namespace Ackee.DataAccess.EfCore.IntegrationTests
{
    public class EfCoreTestRepository : EfCoreRepository<Book, BookId>
    {
        public EfCoreTestRepository(AckeeDbContext dbContext) : base(dbContext)
        {
        }
        public override Task<BookId> GetNextId()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Book>> FindAll()
        {
            return await FindAll(TrueExpression());
        }
        
        public async Task<Book> Find()
        {
            return await Find(TrueExpression());
        }
        private static Expression<Func<Book, bool>> TrueExpression()
        {
            return a => a.Deleted == false;
        }
    }
}