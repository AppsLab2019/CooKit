using System;
using CooKit.Mobile.Models.Images;
using Xamarin.Forms;
using Image = CooKit.Mobile.Models.Images.Image;

namespace CooKit.Mobile.Extensions
{
    public static class ImageExtensions
    {
        public static ImageSource ToImageSource(this Image image)
        {
            return image.Type switch
            {
                ImageType.Uri => ImageSource.FromUri(new Uri(image.Data)),
                _ => throw new NotImplementedException()
            };
        }
    }
}
