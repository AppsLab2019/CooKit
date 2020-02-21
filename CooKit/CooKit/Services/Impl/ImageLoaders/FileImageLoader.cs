using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services.Impl.ImageLoaders
{
    public sealed class FileImageLoader : IImageLoader
    {
        public ImageSource LoadImage(string source) =>
            ImageSource.FromFile(source);

        public async Task<ImageSource> LoadImageAsync(string source) =>
            await Task.Run(() => LoadImage(source));
    }
}
