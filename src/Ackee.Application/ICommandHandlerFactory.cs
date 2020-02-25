namespace Ackee.Application
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> CreateHandler<T>(T command) where T : ICommand;
    }

    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<T> CreateHandler<T>(T command) where T : ICommand
        {
            throw new System.NotImplementedException();
        }
    }
}