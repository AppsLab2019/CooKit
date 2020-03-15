using System;
using System.Globalization;
using Xamarin.Forms;

namespace CooKit.Converters
{
    public sealed class TimeToFormattedMinutesStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan time))
                return null;

            return $"{(int) Math.Round(time.TotalMinutes)} min.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
            throw new NotImplementedException();
    }
}
