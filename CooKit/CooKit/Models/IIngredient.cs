using Xamarin.Forms;

namespace CooKit.Models
{
    public interface IIngredient : IStorable
    {
        string Name { get; }
        ImageSource Icon { get; }
    }
}
