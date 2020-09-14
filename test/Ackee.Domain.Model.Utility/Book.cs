namespace Ackee.Domain.Model.TestUtility
{
    public class Book : AggregateRoot<BookId>
    {
        public Book()
        {

        }
        public Book(long id) : base(new BookId(id), new EventPublisherFake())
        {
            Name = "Name " + id;
        }

        public string Name { get; private set; }



        public void Update(string newName)
        {
            Name = newName;
        }

        public void DoSomethingAndPublishEvent()
        {
            Publish(new BookDoSomethingEvent(this.Id));
        }
    }

    public class BookId : Id<long>
    {
        public BookId(long dbId) : base(dbId)
        {

        }
    }
}