using System;
using System.Collections.Generic;

namespace Mediator
{
    public delegate object ServiceFactory(Type serviceType);

    internal static class ServiceFactoryExtensions
    {
        public static TCast GetInstanceWithCast<TCast>(this ServiceFactory factory, Type type)
        {
            return (TCast) factory(type);
        }

        public static T GetInstance<T>(this ServiceFactory factory)
        {
            return (T) factory(typeof(T));
        }
    }
}