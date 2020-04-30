using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CooKit.Extensions
{
    public static class EnumerableExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            return new ObservableCollection<T>(enumerable);
        }
    }
}
