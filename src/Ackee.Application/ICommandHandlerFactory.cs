namespace Ackee.Application
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> CreateHandler<T>(T command) where T : ICommand;
    }
}