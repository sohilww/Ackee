namespace Ackee.Domain.Model.TestUtility
{
    public class DoSomethingEvent : DomainEvent
    {
        public DoSomethingEvent(IdFake id)
        {
            IdFake = id;
        }

        public IdFake IdFake { get; set; }
    }
}