using Ackee.Core;
using Autofac;

namespace Ackee.Config.Autofac
{
    public class AutofacServiceLocator : IServiceLocator
    {
        private readonly IComponentContext _scoped;

        public AutofacServiceLocator(ILifetimeScope scoped)
        {
            _scoped = scoped;
        }
        public T Resolve<T>()
        {
            return (T) _scoped.Resolve(typeof(T));
        }
    }
}