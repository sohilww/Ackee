﻿namespace Ackee.Domain.Model
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
        public TKey Id { get;protected set; }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TKey>;
            return entity.Id.Equals(this.Id);
        }
    }
}