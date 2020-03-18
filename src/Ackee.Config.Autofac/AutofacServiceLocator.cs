using Ackee.Core;
using Autofac;

namespace Ackee.Config.Autofac
{
    public class AutofacServiceLocator : IServiceLocator
    {
        private readonly IComponentContext _context;

        public AutofacServiceLocator(IComponentContext context)
        {
            _context = context;
        }
        public T Resolve<T>()
        {
            return (T) _context.Resolve(typeof(T));
        }
    }
}