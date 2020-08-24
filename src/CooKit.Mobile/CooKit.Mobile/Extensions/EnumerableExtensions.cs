using System;
using System.Collections.Generic;

namespace CooKit.Mobile.Extensions
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> enumerable, T item)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var index = 0;

            foreach (var currentItem in enumerable)
            {
                if (Equals(currentItem, item))
                    return index;

                index++;
            }

            return -1;
        }
    }
}
