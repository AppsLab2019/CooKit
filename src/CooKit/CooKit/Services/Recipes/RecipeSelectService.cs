using CooKit.Models;

namespace CooKit.Services.Recipes
{
    public sealed class RecipeSelectService : IRecipeSelectService
    {
        private Recipe _recipe;

        public Recipe GetSelectedRecipe()
        {
            if (_recipe is null)
                throw new System.Exception("No recipe selected!");

            return _recipe;
        }

        public void SetSelectedRecipe(Recipe recipe)
        {
            _recipe = recipe;
        }

        public void ClearSelectedRecipe()
        {
            _recipe = null;
        }
    }
}
