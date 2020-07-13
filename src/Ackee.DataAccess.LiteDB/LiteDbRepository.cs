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
        protected readonly LiteRepository Db;
        readonly UncommittedEventHandler _uncommittedEventHandler;
        public abstract Task<TKey> GetNextId();
        protected LiteDbRepository(LiteRepository db)
        {
            Db = db;
            _uncommittedEventHandler= new UncommittedEventHandler(Db);
        }
        public async Task Create(TAggregate aggregate)
        {
            Db.Insert(aggregate);
            _uncommittedEventHandler.Handle(aggregate.UncommittedEvent);
        }

        public async Task Remove(TAggregate aggregate)
        {
            aggregate.Delete();
            await Update(aggregate);
        }

        public async Task<TAggregate> Get(TKey key)
        {
            return Db.FirstOrDefault<TAggregate>(a => a.Id == key);
        }

        protected async Task<TAggregate> Find(Expression<Func<TAggregate, bool>> predicate)
        {
            return GetAggregateDidNotDelete().Where(predicate).FirstOrDefault();
        }

        protected async Task<List<TAggregate>> FindAll(Expression<Func<TAggregate, bool>> predicate)
        {
            return GetAggregateDidNotDelete().Where(predicate).ToList();
        }

        protected ILiteQueryable<TAggregate> GetAggregateDidNotDelete()
        {
            return Db.Query<TAggregate>().Where(a => !a.Deleted);
        }

        protected async Task Update(TAggregate aggregate)
        {
            Db.Update(aggregate);
            _uncommittedEventHandler.Handle(aggregate.UncommittedEvent);
        }
    }
}
