using System.Linq;
using System.Threading.Tasks;
using Ackee.Domain.Model;
using Ackee.Domain.Model.Repositories;
using EventStore.ClientAPI;

namespace Ackee.DataAccess.EventStore
{
    public class EventStoreRepository<TAggregate, TKey> : IRepository<TAggregate, TKey>
        where TKey : Id
        where TAggregate : AggregateRoot<TKey>
    {
        public Task<TKey> GetNextId()
        {
            throw new System.NotImplementedException();
        }

        public Task Create(TAggregate aggregate)
        {
            throw new System.NotImplementedException();
        }

        public Task Remove(TAggregate aggregate)
        {
            throw new System.NotImplementedException();
        }

        public Task<TAggregate> Get(TKey key)
        {
            throw new System.NotImplementedException();
        }

        protected virtual string GetAggregateName()
        {
            var aggregateName = typeof(TAggregate).FullName.Split('.').Last();
            return aggregateName;
        }

        protected string GetStreamName(TKey id)
        {
            return $"{GetAggregateName()}-{id}";
        }
    }

  
}