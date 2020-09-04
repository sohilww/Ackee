using System;
using System.Threading.Tasks;
using Ackee.Application;
namespace Ackee.DataAccess.EfCore
{
    public class EfCommandHandlerDecorator<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decorator;
        private readonly AckeeDbContext _ackeeDbContext;
        public EfCommandHandlerDecorator(ICommandHandler<T> decorator, AckeeDbContext ackeeDbContext)
        {
            _decorator = decorator;
            _ackeeDbContext = ackeeDbContext;
        }
        public async Task Handle(T command)
        {
         
            await _decorator.Handle(command);

            await _ackeeDbContext.SaveChangesAsync();
        }
    }
}