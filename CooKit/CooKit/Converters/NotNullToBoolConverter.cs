using System;
using System.Globalization;
using Xamarin.Forms;

namespace CooKit.Converters
{
    public sealed class NotNullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !(value is null);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            throw new NotSupportedException();
    }
}
