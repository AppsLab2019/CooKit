using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services
{
    public interface IImageLoader
    {
        string Name { get; }

        ImageSource LoadImage(string source);
        Task<ImageSource> LoadImageAsync(string source);
    }
}
