using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public static string ToString<T>(this IEnumerable<T> enumerable, char delimiter, Func<T, string> elementConverter = null)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            elementConverter ??= element => element.ToString();
            return string.Join(delimiter, enumerable.Select(elementConverter));
        }

        // TODO: test performance with StringBuilder
        public static string ToString<T>(this IEnumerable<T> enumerable, char delimiter, char escape,
            Func<T, string> elementConverter = null)
        {
            if (enumerable is null)
                throw new ArgumentNullException(nameof(enumerable));

            if (delimiter == escape)
                throw new ArgumentException("Delimiter and escape characters can't be same!");

            elementConverter ??= element => element.ToString();
            var strings = enumerable.Select(elementConverter);
            var escapedStrings = strings.Select(str => str.Escape(delimiter, escape));
            return string.Join(delimiter, escapedStrings);
        }
    }
}
