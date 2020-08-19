using System;
using System.Reflection;
using Ackee.Config.Loader;

namespace Ackee.Config
{
    public interface IRegistration
    {
        void RegisterControllers(Assembly assembly);

        void RegisterFacadeServices(Assembly assembly);

        void RegisterFacadeServices(Assembly assembly, params Type[] interceptService);

        void RegisterCommandHandlers(Assembly assembly);

        void RegisterRepositories(Assembly assembly);

        void RegisterDomainServices(Assembly assembly);

        void RegisterScoped<TImplementation, TService>();

        void RegisterSingleton<TImplementation, TService>();
        void RegisterSingleton<TImplementation>(TImplementation implementation) where TImplementation : class;
        void RegisterDecorator<TDecorator, TService>() where TDecorator : TService;
        void RegisterDecorator(Type decorator, Type service);

        void RegisterInstanceAsScoped<TImplementation>(Func<IDependencyResolver, TImplementation> register, Action<TImplementation> releaseAction = null);

    }
}
