using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ackee.Domain.Model.Repositories
{
    public interface IRepository
    {
        
    }

    public interface IRepository<T, TKey> : IRepository where T : IAggregateRoot
    {
        Task<TKey> GetNextId();
        Task Create(T aggregate);
        Task Remove(T aggregate);
        Task<T> Get(TKey key);
        Task<T> Find(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindAll(Expression<Func<T, bool>> predicate);
    }
}