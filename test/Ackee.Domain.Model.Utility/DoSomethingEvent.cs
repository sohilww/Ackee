namespace Ackee.Domain.Model.TestUtility
{
    public class DoSomethingEvent : DomainEvent
    {
        public DoSomethingEvent(IdFake id)
        {
            Id = id;
        }

        public DoSomethingEvent(BookId bookId)
        {
            
        }

        public IdFake Id { get; set; }
    }
}