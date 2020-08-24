using System;
using System.Globalization;
using CooKit.Mobile.Extensions;
using Xamarin.Forms;
using Image = CooKit.Mobile.Models.Images.Image;

namespace CooKit.Mobile.Converters
{
    public class ImageToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Image image
                ? image.ToImageSource()
                : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
