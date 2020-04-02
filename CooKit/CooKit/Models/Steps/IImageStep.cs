using Xamarin.Forms;

namespace CooKit.Models.Steps
{
    public interface IImageStep : IStep
    {
        ImageSource Image { get; }
    }
}
