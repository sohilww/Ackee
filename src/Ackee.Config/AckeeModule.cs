using Ackee.Application;
using Ackee.Core;

namespace Ackee.Config
{
    public class AckeeModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            registration.RegisterScoped<CommandBus, ICommandBus>();
            registration.RegisterScoped<CommandHandlerFactory, ICommandHandlerFactory>();
            registration.RegisterSingleton<ExceptionHandler, IExceptionHandler>();
        }
    }
}