using System.Threading.Tasks;

namespace Ackee.Config.Loader
{
    public interface IDependencyResolver
    {
        T Resolve<T>();
    }
}