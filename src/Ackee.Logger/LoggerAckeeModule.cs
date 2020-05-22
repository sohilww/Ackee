using Ackee.Application;
using Ackee.Config;

namespace Ackee.Logger
{
    public class LoggerAckeeModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            registration.RegisterDecorator(typeof(LoggingDecoratorCommandHandler<>),typeof(ICommandHandler<>));
        }
    }
}