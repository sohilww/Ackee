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
        public abstract Task<TKey> GetNextId();
        protected string _connectionString = Environment.CurrentDirectory + "liteDb.db";

        public LiteDbRepository()
        {
        }
        public async Task Create(TAggregate aggregate)
        {
            using (LiteRepository db = new LiteRepository(_connectionString))
            {
                db.Insert(aggregate);
            }
        }

        public async Task Remove(TAggregate aggregate)
        {
            using (LiteRepository db = new LiteRepository(_connectionString))
            {
                aggregate.Delete();
                db.Update(aggregate);
            }
        }

        public async Task<TAggregate> Get(TKey key)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                return db.FirstOrDefault<TAggregate>(a => a.Id == key);
            }
        }


        public async Task<TAggregate> Find(Expression<Func<TAggregate, bool>> predicate)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                return GetAggregateDidNotDelete(db).Where(a=>!a.Deleted).Where(predicate).FirstOrDefault();
            }
        }

        public async Task<List<TAggregate>> FindAll(Expression<Func<TAggregate, bool>> predicate)
        {
            using (var db = new LiteRepository(_connectionString))
            {
                return GetAggregateDidNotDelete(db).Where(a=>!a.Deleted).Where(predicate).ToList();
            }
        }

        protected ILiteQueryable<TAggregate> GetAggregateDidNotDelete(LiteRepository db)
        {
            return db.Query<TAggregate>().Where(a => !a.Deleted);
        }
    }
}
