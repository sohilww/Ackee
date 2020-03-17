using System;
using System.Threading.Tasks;

namespace Ackee.Application
{
    public interface ICommandBus
    {
        Task Dispatch<T>(T command);
    }
}
