using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services.Impl.ImageLoaders
{
    public sealed class FileImageLoader : IImageLoader
    {
        public string Name => "FileImageLoader";

        public Task<ImageSource> LoadImageAsync(string source) =>
            Task.Run(() => ImageSource.FromFile(source));
    }
}
