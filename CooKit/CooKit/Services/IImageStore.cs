using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services
{
    // TODO: rename class
    public interface IImageStore
    {
        void RegisterLoader(string loaderName, IImageLoader instance);

        ImageSource LoadImage(string loaderName, string source);
        Task<ImageSource> LoadImageAsync(string loaderName, string source);
    }
}
