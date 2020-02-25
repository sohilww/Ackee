using Ackee.Core;

namespace Ackee.Application
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceLocator _serviceLocator;

        public CommandHandlerFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }
        public ICommandHandler<T> CreateHandler<T>(T command) where T : ICommand
        {
            var handler = _serviceLocator.Resolve<ICommandHandler<T>>();
            return handler;
        }
    }
}