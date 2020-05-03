namespace Ackee.Domain.Model
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>,
        IAggregateRoot where TKey : Id
    {
        protected AggregateRoot(TKey id) : base(id)
        {
        }

        protected AggregateRoot() : base()
        {

        }

        
    }
}