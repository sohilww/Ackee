using System.Threading.Tasks;
using Ackee.Application;
namespace Ackee.DataAccess.EfCore
{
    public class EfCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _commandHandler;
        private readonly AckeeDbContext _ackeeDbContext;
        public EfCommandHandlerDecorator(ICommandHandler<T> commandHandler, AckeeDbContext ackeeDbContext)
        {
            _commandHandler = commandHandler;
            _ackeeDbContext = ackeeDbContext;
        }
        public async Task Handle(T command)
        {
            await _commandHandler.Handle(command);
            await _ackeeDbContext.SaveChangesAsync();
        }
    }
}