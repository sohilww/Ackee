using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ackee.Domain.Model;
using Ackee.Domain.Model.Repositories;

namespace Ackee.DataAccess.ListDatabase
{
    public class ListDatabase<TAggregate,TKey> :
        IRepository<TAggregate,TKey> 
        where TAggregate:AggregateRoot<TKey>
        where TKey:Id
    {
        public static Dictionary<TKey, TAggregate> Context=new Dictionary<TKey, TAggregate>();

        public Task<TKey> GetNextId()
        {
            throw new NotImplementedException();
        }
        

        public async Task Create(TAggregate aggregate)
        {
            Context.Add(aggregate.Id,aggregate);
        }

        public async Task Remove(TAggregate aggregate)
        {
            Context.Remove(aggregate.Id);
        }

        public async Task<TAggregate> Get(TKey key)
        {
            return Context[key];
        }

        public Task<TAggregate> Find(Expression<Func<TAggregate, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<TAggregate>> FindAll(Expression<Func<TAggregate, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Context.Clear();
        }
    }
}
