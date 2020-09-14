using Ackee.Domain.Model;
using Ackee.Domain.Model.EventManager;
using Ackee.Domain.Model.Repositories;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ackee.DataAccess.LiteDB
{
    public abstract class LiteDbRepository<TAggregate, TKey> : IRepository<TAggregate, TKey>
        where TKey : Id
        where TAggregate : AggregateRoot<TKey>
    {
        protected readonly LiteRepository Db;
        private readonly IEventPublisher _eventPublisher;
        readonly UncommittedEventHandler _uncommittedEventHandler;
        public abstract Task<TKey> GetNextId();
        protected LiteDbRepository(LiteRepository db, IEventPublisher eventPublisher)
        {
            Db = db;
            _eventPublisher = eventPublisher;
            _uncommittedEventHandler = new UncommittedEventHandler(Db);
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
            var aggregate = Db.FirstOrDefault<TAggregate>(a => a.Id == key);

            SetPublisher(aggregate);
            return aggregate;
        }



        protected async Task<TAggregate> Find(Expression<Func<TAggregate, bool>> predicate)
        {
            var aggregate = GetAggregateDidNotDelete().Where(predicate).FirstOrDefault();

            SetPublisher(aggregate);

            return aggregate;
        }

        protected async Task<List<TAggregate>> FindAll(Expression<Func<TAggregate, bool>> predicate)
        {
            var aggregates = GetAggregateDidNotDelete().Where(predicate).ToList();

            aggregates.ForEach(SetPublisher);

            return aggregates;
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

        private void SetPublisher(TAggregate aggregate)
        {
            aggregate.SetPublisher(_eventPublisher);
        }
    }
}
