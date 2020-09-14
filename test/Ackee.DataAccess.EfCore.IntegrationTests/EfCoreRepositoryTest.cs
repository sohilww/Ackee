using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ackee.DataAccess.EfCore.IntegrationTests
{
    public class EfCoreRepositoryTest : IDisposable
    {
        private readonly EfCoreTestDbContext _testDb;
        private readonly EfCoreTestRepository _repository;
        private readonly IDbContextTransaction _tran;

        public EfCoreRepositoryTest()
        {
            _testDb = new EfCoreTestDbContext();
            _tran = _testDb.Database.BeginTransaction(IsolationLevel.ReadCommitted);
            _repository = new EfCoreTestRepository(_testDb);
        }

        [Fact]
        public async Task Create_book_aggregate()
        {
            var book = BookFactoryTest.Create();

            await _repository.Create(book);

            await _testDb.SaveChangesAsync();

            var first = _testDb.Books.First();

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
        [Fact]
        public async Task save_uncommitted_events()
        {
            var book = BookFactoryTest.Create();
            book.DoSomethingAndPublishEvent();

            await _repository.Create(book);

            await _testDb.SaveChangesAsync();

            book.UncommittedEvent.Should().BeEmpty();
            _testDb.Events.AsQueryable().ToList().Should().NotBeEmpty();
        }

        [Fact]
        public async Task soft_delete()
        {
            var book = BookFactoryTest.Create();

            await InsertABook(book);

            await _repository.Remove(book);

            await _testDb.SaveChangesAsync();

            var readBook = _testDb.Books.FirstOrDefault(a => a.Id == book.Id);

            readBook.Deleted.Should().BeTrue();

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
            await _testDb.Books.AddAsync(book);
            await _testDb.SaveChangesAsync();
        }

        public void Dispose()
        {
            _tran.Dispose();
            _testDb.Dispose();
        }
    }
};