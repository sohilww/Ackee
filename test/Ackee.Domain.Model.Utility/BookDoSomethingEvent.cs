namespace Ackee.Domain.Model.TestUtility
{
    public class BookDoSomethingEvent : DomainEvent
    {
        public BookDoSomethingEvent() :base() { }

        public BookDoSomethingEvent(BookId bookId)
        {
            BookId = bookId;
        }
        public BookId BookId { get;private set; }
        
    }
}