using Ackee.Application;
using Ackee.Config;
using Microsoft.Extensions.Logging;

namespace Ackee.Logger
{
    public class LoggerAckeeModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            registration.RegisterInstanceAsScoped(resolver => resolver.Resolve<ILoggerFactory>().CreateLogger(""));
            registration.RegisterDecorator(typeof(LoggingDecoratorCommandHandler<>),typeof(ICommandHandler<>));
        }
    }
}