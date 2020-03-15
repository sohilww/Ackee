using System;
using System.Threading.Tasks;

namespace Ackee.Application
{
    public interface ICommandBus
    {
        Task Dispatch(ICommand command);
    }
}
