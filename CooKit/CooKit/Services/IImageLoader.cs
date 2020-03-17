using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services
{
    public interface IImageLoader
    {
        string Name { get; }

        Task<ImageSource> LoadImageAsync(string source);
    }
}
