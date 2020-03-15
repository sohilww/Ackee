using System.Threading.Tasks;

namespace Ackee.Application
{
    public interface ICommandHandler { }
    public interface ICommandHandler<T> where T :ICommand
    {
        Task Handel(T command);
    }
}