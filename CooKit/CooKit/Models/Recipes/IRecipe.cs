using Xamarin.Forms;

namespace CooKit.Models.Recipes
{
    public interface IRecipe
    {

        string Name { get; }
        int Difficulty { get; }
        int TimeNeeded { get; }

        ImageSource Image { get; }

    }
}