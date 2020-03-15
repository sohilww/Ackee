using Ackee.Core;
using Ackee.Core.Exceptions;

namespace Ackee.Application
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceLocator _serviceLocator;

        public CommandHandlerFactory(IServiceLocator serviceLocator)
        {
            GuardAgainstNull(serviceLocator);

            _serviceLocator = serviceLocator;
        }

        private static void GuardAgainstNull(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
                throw new ArgumentNullAckeeException();
        }

        public ICommandHandler<T> CreateHandler<T>(T command) where T : ICommand
        {
            var handler = _serviceLocator.Resolve<ICommandHandler<T>>();
            return handler;
        }
    }
}