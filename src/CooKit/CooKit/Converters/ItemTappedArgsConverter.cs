using System;
using System.Globalization;
using Xamarin.Forms;

namespace CooKit.Converters
{
    public sealed class ItemTappedArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ItemTappedEventArgs args))
                return null;

            return args.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
