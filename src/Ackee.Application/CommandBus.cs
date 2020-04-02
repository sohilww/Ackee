using System.Threading.Tasks;
using Ackee.Core.Exceptions;

namespace Ackee.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _factory;

        public CommandBus(ICommandHandlerFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullAckeeException();;
            _factory = factory;
        }
        public async Task Dispatch<T>(T command)
        {
            var handler = _factory.CreateHandler(command);
            await handler.Handle(command);
        }
    }
}