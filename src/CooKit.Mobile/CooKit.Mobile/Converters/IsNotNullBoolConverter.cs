using System;
using System.Globalization;
using Xamarin.Forms;

namespace CooKit.Mobile.Converters
{
    public class IsNotNullBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
