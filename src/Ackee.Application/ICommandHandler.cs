using System.Threading.Tasks;

namespace Ackee.Application
{
    public interface ICommandHandler { }
    public interface ICommandHandler<T>
    {
        Task Handel(T command);
    }
}