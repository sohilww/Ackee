using Ackee.Domain.Model;
using Ackee.Domain.Model.EventManager;
using Ackee.Domain.Model.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ackee.DataAccess.EfCore
{
    public abstract class EfCoreRepository<TAggregate, TKey> : IRepository<TAggregate, TKey>
        where TKey : Id
        where TAggregate : AggregateRoot<TKey>
    {
        protected readonly AckeeDbContext _dbContext;
        private readonly IEventPublisher _eventPublisher;

        protected EfCoreRepository(AckeeDbContext dbContext, IEventPublisher eventPublisher)
        {
            _dbContext = dbContext;
            _eventPublisher = eventPublisher;
        }

        public abstract Task<TKey> GetNextId();

        public async Task Create(TAggregate aggregate)
        {
            await _dbContext.AddAsync(aggregate);
        }

        public async Task Remove(TAggregate aggregate)
        {
            aggregate.Delete();
        }
        public async Task<TAggregate> Get(TKey key)
        {
            var aggregate = await _dbContext.Set<TAggregate>().FirstOrDefaultAsync(a => a.Id == key);

            SetPublisher(aggregate);
            return aggregate;
        }
        protected async Task<TAggregate> Find(Expression<Func<TAggregate, bool>> predicate)
        {
            var aggregate = await GetAggregateDidNotDelete().FirstOrDefaultAsync(predicate);

            SetPublisher(aggregate);
            return aggregate;
        }

        protected async Task<List<TAggregate>> FindAll(Expression<Func<TAggregate, bool>> predicate)
        {
            var aggregates = await GetAggregateDidNotDelete().Where(predicate).ToListAsync();

            aggregates.ForEach(SetPublisher);
            return aggregates;

        }

        protected IQueryable<TAggregate> GetAggregateDidNotDelete()
        {
            return _dbContext.Set<TAggregate>().Where(a => !a.Deleted);
        }

        private void SetPublisher(TAggregate aggregate)
        {
            aggregate?.SetPublisher(_eventPublisher);
        }
    }
}