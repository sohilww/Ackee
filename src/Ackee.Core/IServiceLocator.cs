using System;

namespace Ackee.Core
{
    public interface IServiceLocator
    {
        T Resolve<T>();
    }
}