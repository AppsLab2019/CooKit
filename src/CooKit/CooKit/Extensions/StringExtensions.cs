using System;
using System.Collections.Generic;
using System.Text;

namespace CooKit.Extensions
{
    public static class StringExtensions
    {
        public static string Escape(this string source, char delimiter, char escape)
        {
            return Escape(source, delimiter.ToString(), escape.ToString());
        }

        public static string Escape(this string source, string delimiter, string escape)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (source.Length == 0)
                return string.Empty;

            var builder = new StringBuilder(source);
            builder.Replace(escape, $"{escape}{escape}");
            builder.Replace(delimiter, $"{escape}{delimiter}");
            return builder.ToString();
        }

        public static IEnumerable<string> SplitWithEscape(this string source, char delimiter, char escape, 
            StringSplitOptions options = StringSplitOptions.None)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (source == string.Empty)
                yield break;

            if (delimiter == escape)
                throw new ArgumentException("Delimiter and escape characters can't be same!");

            var builder = new StringBuilder();
            var isEscaped = false;

            foreach (var character in source)
            {
                if (isEscaped)
                {
                    builder.Append(character);
                    isEscaped = false;
                }
                else if (character == delimiter)
                {
                    if (builder.Length == 0)
                        if (options == StringSplitOptions.None)
                            yield return string.Empty;
                        else
                            continue;

                    var result = builder.ToString();
                    builder.Clear();

                    yield return result;
                }
                else if (character == escape)
                    isEscaped = true;
                else
                    builder.Append(character);
            }

            if (builder.Length != 0 || options == StringSplitOptions.None)
                yield return builder.ToString();
        }
    }
}
