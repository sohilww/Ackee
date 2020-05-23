using Ackee.Application;
using Ackee.Config;
using Microsoft.Extensions.Logging;

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