using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Domain.Model.TestUtility;
using FluentAssertions;
using LiteDB;
using Xunit;

namespace Ackee.DataAccess.LiteDB.IntegrationTest
{
    public class LiteDbRepositoryTest : LiteDbBaseClassTest, IDisposable
    {
        private readonly BookRepositoryFake _bookRepository;
        private readonly AggregateRootFake _aggregate=  new AggregateRootFake(new IdFake(1));

        public LiteDbRepositoryTest()
        {
            _bookRepository = new BookRepositoryFake(Db);
        }
        [Fact]
        public async Task should_generate_new_id()
        {
            var id = await _bookRepository.GetNextId();

            id.DbId.Should().NotBe(0);
        }

        [Fact]
        public async Task should_create_book_aggregate()
        {
            var book = BookFactoryTest.Create();

            await _bookRepository.Create(book);

            var insertedBook = await _bookRepository.Get(book.Id);

            insertedBook.Should().Be(book);
        }
        [Fact]
        public async Task If_aggregate_has_events_it_saves_in_events_collection()
        {
            _aggregate.DoSomethingAndPublishEvent();

            var uncommittedEvent = _aggregate.UncommittedEvent.First();

            Db.Insert(uncommittedEvent);

            _aggregate.UncommittedEvent.Should().NotBeEmpty();

        }

        [Fact]
        public async Task should_remove_book_aggregate()
        {
            var book = await InsertBook();

            await _bookRepository.Remove(book);

            var insertedBook = await _bookRepository.Get(book.Id);

            insertedBook.Deleted.Should().BeTrue();
        }

        [Fact]
        public async Task should_findAll_books()
        {
            await InsertTwoBooks();

            var books = await _bookRepository.FindAll(a => true);

            books.Should().HaveCount(2);
        }

        [Fact]
        public async Task should_find_book_with_name()
        {
            var book =await InsertBook();

            var foundBook= await _bookRepository.Find(a => a.Name == book.Name);

            foundBook.Should().Be(book);
        }


        [Fact]
        public async Task should_not_find_deleted_book()
        {
            var book =await InsertBookAndDelete();

            var foundBook = await _bookRepository.Find(a => a.Id == book.Id);

            foundBook.Should().BeNull();
        }
        [Fact]
        public async Task should_not_findAll_deleted_book()
        {
            var book = await InsertBookAndDelete();

            var foundBook = await _bookRepository.FindAll(a => a.Id == book.Id);

            foundBook.Should().HaveCount(0);
        }

       

        private async Task InsertTwoBooks()
        {
            await InsertBook();
            await InsertBook();
        }

        private async Task<Book> InsertBook()
        {
            var book = BookFactoryTest.Create();

            await _bookRepository.Create(book);
            return book;
        }

        private async Task<Book> InsertBookAndDelete()
        {
            var book = await InsertBook();
            await _bookRepository.Remove(book);
            return book;
        }

        public void Dispose()
        {
            Db.Dispose();
            File.Delete(ConnectionString);
        }
    }
}
