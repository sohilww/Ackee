using Ackee.Application;
using Ackee.Core;
using Ackee.Domain.Model.EventManager;

namespace Ackee.Config
{
    public class AckeeModule : IAckeeModule
    {
        public void Load(IRegistration registration)
        {
            registration.RegisterScoped<CommandBus, ICommandBus>();
            registration.RegisterScoped<CommandHandlerFactory, ICommandHandlerFactory>();
            registration.RegisterSingleton<ExceptionHandler, IExceptionHandler>();


            registration.RegisterScoped<EventAggregator, EventAggregator>();

            registration.RegisterInstanceAsScoped<IEventSubscriber>
                (resolver => resolver.Resolve<EventAggregator>());

            registration.RegisterInstanceAsScoped<IEventPublisher>
                (resolver => resolver.Resolve<EventAggregator>());
        }
    }
}