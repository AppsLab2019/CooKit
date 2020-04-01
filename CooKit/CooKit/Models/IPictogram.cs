using Xamarin.Forms;

namespace CooKit.Models
{
    public interface IPictogram : IStorable
    {
        string Name { get; }
        string Description { get; }
        ImageSource Icon { get; }
    }
}
