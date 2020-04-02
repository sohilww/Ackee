using System.Threading.Tasks;

namespace Ackee.Application
{
    public interface ICommandHandler { }
    public interface ICommandHandler<T>
    {
        Task Handle(T command);
    }
}