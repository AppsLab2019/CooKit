using System;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace CooKit.Converters
{
    public sealed class CutStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Untested!");

            if (!(value is string text))
                return null;

            if (!(parameter is int count))
                return value;

            if (text.Length <= count)
                return text;

            var length = text.Length;
            var builder = new StringBuilder(text);

            builder.Remove(count, length - count);
            builder.Append("...");

            return builder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
