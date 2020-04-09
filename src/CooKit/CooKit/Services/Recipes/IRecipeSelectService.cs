using CooKit.Models;

namespace CooKit.Services.Recipes
{
    public interface IRecipeSelectService
    {
        Recipe GetSelectedRecipe();
        void SetSelectedRecipe(Recipe recipe);
        void ClearSelectedRecipe();
    }
}
