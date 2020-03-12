using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services.Impl.ImageLoaders
{
    public sealed class UriImageLoader : IImageLoader
    {
        public string Name => "UriImageLoader";

        public ImageSource LoadImage(string source) =>
            ImageSource.FromUri(new Uri(source));

        public async Task<ImageSource> LoadImageAsync(string source) =>
            await Task.Run(() => ImageSource.FromUri(new Uri(source)));
    }
}
