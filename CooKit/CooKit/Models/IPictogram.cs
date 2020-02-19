using Xamarin.Forms;

namespace CooKit.Models
{
    public interface IPictogram
    {
        string Name { get; }
        string Description { get; }
        ImageSource Image { get; }
    }
}
