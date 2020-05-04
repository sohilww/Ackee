using Ackee.Application;

namespace Ackee.Config
{
    public class AckeeModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            registration.RegisterScoped<CommandBus, ICommandBus>();
            registration.RegisterScoped<CommandHandlerFactory, ICommandHandlerFactory>();
        }
    }
}