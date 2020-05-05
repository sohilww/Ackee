using Ackee.Config.Loader;
using Autofac;

namespace Ackee.Config.Autofac
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope _scope;

        public AutofacDependencyResolver(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public T Resolve<T>()
        {
            return _scope.Resolve<T>();
        }
    }
}