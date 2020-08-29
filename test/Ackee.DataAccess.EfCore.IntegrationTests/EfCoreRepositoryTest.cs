using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Xunit;

namespace Ackee.DataAccess.EfCore.IntegrationTests
{
    public class EfCoreRepositoryTest : IDisposable
    {
        private readonly EfCoreDbContext _db;
        private readonly EfCoreTestRepository _repository;
        private readonly IDbContextTransaction _tran;

        public EfCoreRepositoryTest()
        {
            _db = new EfCoreDbContext();
            _tran = _db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            _repository = new EfCoreTestRepository(_db);
        }

        [Fact]
        public async Task Create_book_aggregate()
        {
            var book = BookFactoryTest.Create();

            await _repository.Create(book);

            await _db.SaveChangesAsync();

            var first = _db.Books.First();

            first.Should().NotBeNull();
        }

        [Fact]
        public async Task read_a_book_from_database()
        {
            var book = BookFactoryTest.Create();
            await InsertABook(book);

            var first = await _repository.Get(book.Id);

            first.Should().NotBeNull();
        }

        [Fact]
        public async Task reads_book_from_database()
        {
            await InsertTwoBooks();

            var allBook = await _repository.FindAll();

            allBook.Count.Should().Be(2);
        }

        [Fact]
        public async Task find_a_book_from_database()
        {
            var book = BookFactoryTest.Create();
            await InsertABook(book);

            var oneBook = await _repository.Find();

            oneBook.Should().Be(book);
        }

        private async Task InsertTwoBooks()
        {
            for (int i = 0; i < 2; i++)
            {
                var book = BookFactoryTest.Create();
                await InsertABook(book);
            }
        }

        private async Task InsertABook(Book book = null)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _tran.Dispose();
            _db.Dispose();
        }
    }
}