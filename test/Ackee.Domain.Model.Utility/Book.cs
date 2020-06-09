using System;

namespace Ackee.Domain.Model.TestUtility
{
    public class Book : AggregateRoot<BookId>
    {
        public Book()
        {
            
        }
        public Book(long id) : base(new BookId(id))
        {
            Name = "Name " + id;
        }

        public string Name { get;private set; }
        


        public void Update(string newName)
        {
            Name = newName;
        }
    }

    public class BookId : Id<long>
    {
        public BookId():base()
        {
            
        }
        public BookId(long id):base(id)
        {
            
        }
    }
}