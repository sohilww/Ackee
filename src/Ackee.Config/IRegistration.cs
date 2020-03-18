using System.Reflection;

namespace Ackee.Config
{
    public interface IRegistration
    {
        void RegisterControllers(Assembly assembly);

        void RegisterFacadeServices(Assembly assembly);

        void RegisterCommandHandlers(Assembly assembly);

        void RegisterRepositories(Assembly assembly);

        void RegisterScoped<TImplementation, TService>();
        void RegisterSingleton<TImplementation, TService>();
    }
}
