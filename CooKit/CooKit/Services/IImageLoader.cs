using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services
{
    public interface IImageLoader
    {
        ImageSource LoadImage(string source);
        Task<ImageSource> LoadImageAsync(string source);
    }
}
