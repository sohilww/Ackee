namespace Ackee.Domain.Model
{
    public abstract class Id :ValueObject
    {

    }
    public abstract class Id<TKey> : Id
    {
        protected Id(TKey id)
        {
            DbId = id;
        }
        public TKey DbId { get;protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;

            var id = obj as Id<TKey>;
            var result= this.DbId.Equals(id.DbId);
            return result;
        }

        public override int GetHashCode()
        {
            return this.DbId.GetHashCode();
        }
    }
}