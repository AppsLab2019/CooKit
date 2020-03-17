using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services
{
    // TODO: rename class
    public interface IImageStore
    {
        IReadOnlyList<IImageLoader> RegisteredLoaders { get; }

        void RegisterLoader(IImageLoader instance);
        Task<ImageSource> LoadImageAsync(string loaderName, string source);
    }
}
