using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ackee.Domain.Model;
using Ackee.Domain.Model.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ackee.DataAccess.EfCore
{
    public abstract class EfCoreRepository<TAggregate, TKey> : IRepository<TAggregate, TKey>
        where TKey : Id
        where TAggregate : AggregateRoot<TKey>
    {
        private readonly AckeeDbContext _dbContext;

        protected EfCoreRepository(AckeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract Task<TKey> GetNextId();

        public async Task Create(TAggregate aggregate)
        {
            await _dbContext.AddAsync(aggregate);
        }

        public async Task Remove(TAggregate aggregate)
        {
            _dbContext.Remove(aggregate);
        }
        public async Task<TAggregate> Get(TKey key)
        {
            return await _dbContext.Set<TAggregate>().FirstOrDefaultAsync(a => a.Id == key);
        }
        protected async Task<TAggregate> Find(Expression<Func<TAggregate, bool>> predicate)
        {
            return await GetAggregateDidNotDelete().FirstOrDefaultAsync(predicate);
        }

        protected async Task<List<TAggregate>> FindAll(Expression<Func<TAggregate, bool>> predicate)
        {
            return await GetAggregateDidNotDelete().Where(predicate).ToListAsync();
        }
        protected IQueryable<TAggregate> GetAggregateDidNotDelete()
        {
            return _dbContext.Set<TAggregate>().Where(a => !a.Deleted);
        }
    }
}