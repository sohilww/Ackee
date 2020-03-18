using Ackee.Application;

namespace Ackee.Config
{
    public class AckeeModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            registration.RegisterSingleton<CommandBus, ICommandBus>();
            registration.RegisterSingleton<CommandHandlerFactory, ICommandHandlerFactory>();
        }
    }
}