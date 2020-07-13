using System.Threading.Tasks;

namespace Ackee.Domain.Model.Repositories
{
    public interface IRepository
    {

    }

    public interface IRepository<T, TKey> :
        IRepository where T : IAggregateRoot
        where TKey : Id

    {
        Task<TKey> GetNextId();
        Task Create(T aggregate);
        Task Remove(T aggregate);
        Task<T> Get(TKey key);
    }
}