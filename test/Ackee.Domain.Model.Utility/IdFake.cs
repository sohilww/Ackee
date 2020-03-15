namespace Ackee.Domain.Model.TestUtility
{
    public class IdFake :Id<long>
    {
        public IdFake(long id) : base(id)
        {
        }
    }
}