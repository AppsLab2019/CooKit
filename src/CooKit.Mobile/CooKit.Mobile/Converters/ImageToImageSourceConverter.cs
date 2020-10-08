using System;
using System.Globalization;
using System.IO;
using CooKit.Mobile.Models.Images;
using CooKit.Mobile.Providers.ResourcePath;
using Xamarin.Forms;
using Image = CooKit.Mobile.Models.Images.Image;

namespace CooKit.Mobile.Converters
{
    public class ImageToImageSourceConverter : IValueConverter
    {
        private readonly IResourcePathProvider _pathProvider;

        public ImageToImageSourceConverter(IResourcePathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Image image))
                return value;

            var data = image.Data;

            return image.Type switch
            {
                ImageType.Uri => ImageSource.FromUri(new Uri(data)),
                ImageType.File => ImageSource.FromFile(data),
                ImageType.Resource => ImageSource.FromResource(data),
                ImageType.LocalResource => FromLocalResource(data),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private ImageSource FromLocalResource(string data)
        {
            var path = _pathProvider.GetResourceFolderPath();
            return Path.Combine(path, data);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
