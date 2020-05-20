using System;

namespace CooKit.Extensions
{
    public static class WeakReferenceExtensions
    {
        public static T GetValueOrNull<T>(this WeakReference<T> weakRef) where T : class
        {
            if (weakRef is null)
                throw new ArgumentNullException(nameof(weakRef));

            return weakRef.TryGetTarget(out var content) ? content : null;
        }
    }
}
