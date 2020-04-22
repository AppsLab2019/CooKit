using CooKit.Models.Recipes;

namespace CooKit.Services.Recipes
{
    public interface IRecipeSelectService
    {
        IRecipe GetSelectedRecipe();
        void SetSelectedRecipe(IRecipe recipe);
        void ClearSelectedRecipe();
    }
}
