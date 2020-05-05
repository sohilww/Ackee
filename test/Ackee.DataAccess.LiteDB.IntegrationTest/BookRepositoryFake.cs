using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;
using LiteDB;

namespace Ackee.DataAccess.LiteDB.IntegrationTest
{
    public class BookRepositoryFake : LiteDbRepository<Book, BookId>
    {
        public BookRepositoryFake(LiteRepository db) : base(db)
        {
        }
        public async override Task<BookId> GetNextId()
        {
            return new BookId(10);
        }


        public async Task<Book> Find(Expression<Func<Book, bool>> predicate)
        {
            return await base.Find(predicate);
        }
        public async Task<List<Book>> FindAll(Expression<Func<Book, bool>> predicate)
        {
            return await base.FindAll(predicate);
        }

       
    }
}