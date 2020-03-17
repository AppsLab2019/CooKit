using Xamarin.Forms;

namespace CooKit.Models.Steps
{
    public interface IBigImageRecipeStep : IRecipeStep
    {
        ImageSource Image { get; }
    }
}
