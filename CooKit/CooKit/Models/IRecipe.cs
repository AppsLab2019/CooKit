using Xamarin.Forms;

namespace CooKit.Models
{
    public interface IRecipe
    {
        string Name { get; }
        string Description { get; }
        ImageSource MainImage { get; }
    }
}
