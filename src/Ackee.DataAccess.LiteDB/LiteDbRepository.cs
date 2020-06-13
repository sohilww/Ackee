using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ackee.Domain.Model;
using Ackee.Domain.Model.Repositories;
using LiteDB;

namespace Ackee.DataAccess.LiteDB
{
    public abstract class LiteDbRepository<TAggregate, TKey> : IRepository<TAggregate, TKey>
        where TKey : Id
        where TAggregate : AggregateRoot<TKey>
    {
        protected readonly LiteRepository _db;
        UncommittedEventHandler _uncommittedEventHandler;
        public abstract Task<TKey> GetNextId();
        protected LiteDbRepository(LiteRepository db)
        {
            _db = db;
            _uncommittedEventHandler= new UncommittedEventHandler(_db);
        }
        public async Task Create(TAggregate aggregate)
        {
            _db.Insert(aggregate);
            _uncommittedEventHandler.Handle(aggregate.UncommittedEvent);
        }

        public async Task Remove(TAggregate aggregate)
        {
            aggregate.Delete();
            _db.Update(aggregate);
        }

        public async Task<TAggregate> Get(TKey key)
        {
            return _db.FirstOrDefault<TAggregate>(a => a.Id == key);
        }

        protected async Task<TAggregate> Find(Expression<Func<TAggregate, bool>> predicate)
        {
            return GetAggregateDidNotDelete().Where(a => !a.Deleted).Where(predicate).FirstOrDefault();
        }

        protected async Task<List<TAggregate>> FindAll(Expression<Func<TAggregate, bool>> predicate)
        {
            return GetAggregateDidNotDelete().Where(a => !a.Deleted).Where(predicate).ToList();
        }

        protected ILiteQueryable<TAggregate> GetAggregateDidNotDelete()
        {
            return _db.Query<TAggregate>().Where(a => !a.Deleted);
        }
    }

    public class UncommittedEventHandler
    {
        private readonly LiteRepository _db;
        public UncommittedEventHandler(LiteRepository db)
        {
            _db = db;
        }
        public void Handle(ICollection<IDomainEvent> uncommittedEvents)
        {
            foreach (var uncommittedEvent in uncommittedEvents)
            {
                _db.Insert(uncommittedEvent, "Events");
            }
            
        }
    }
}
