using Ackee.Application;
using Ackee.Config.Loader;
using Ackee.Core;
using Autofac;

namespace Ackee.Config.Autofac
{
    public class AutofacAckeeModule :IAckeeIocModule
    {
        private readonly ContainerBuilder _builder;

        public AutofacAckeeModule(ContainerBuilder builder)
        {
            _builder = builder;
        }
        public void Load(IRegistration registration)
        {
            registration.RegisterSingleton<AutofacServiceLocator,IServiceLocator>();
        }

        public IRegistration CreateRegistry()
        {
            return new AutofacRegistration(_builder);
        }
    }
}