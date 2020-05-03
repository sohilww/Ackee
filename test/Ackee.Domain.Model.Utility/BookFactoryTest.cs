using System;

namespace Ackee.Domain.Model.TestUtility
{
    public class BookFactoryTest
    {
        public static Book Create()
        {
            return new Book(new Random().Next(int.MinValue,int.MaxValue));
        }

        public static Book CreateWithId(long id)
        {
            return new Book(id);
            
        }
    }
}