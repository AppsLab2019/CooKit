using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services.Impl.ImageLoaders
{
    public sealed class UriImageLoader : IImageLoader
    {
        public string Name => "UriImageLoader";

        public Task<ImageSource> LoadImageAsync(string source) =>
            Task.Run(() => ImageSource.FromUri(new Uri(source)));
    }
}
