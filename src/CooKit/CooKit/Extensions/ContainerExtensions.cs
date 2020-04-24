using System;
using Autofac;

namespace CooKit.Extensions
{
    public static class ContainerExtensions
    {
        public static Lazy<T> LazyResolve<T>(this IContainer container)
        {
            if (container is null)
                throw new ArgumentNullException(nameof(container));

            return new Lazy<T>(container.Resolve<T>());
        }
    }
}
