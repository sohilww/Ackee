using System;

namespace Ackee.Domain.Model
{
    public abstract class Entity<TKey>
    {
        protected Entity()
        {

        }
        protected Entity(TKey id)
        {
            Id = id;
        }
        public TKey Id { get; protected set; }
        

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TKey>;
            return entity.Id.Equals(this.Id);
        }
        public bool Deleted { get; protected set; } = false;

        //todo: Technical Debt
        //Todo: it's not good pattern for safe delete
        public void Delete()
        {
            Deleted = true;
        }
        public DateTime CreatingDateTime { get; }
    }
}