using CooKit.Models.Recipes;

namespace CooKit.Services.Recipes
{
    public sealed class RecipeSelectService : IRecipeSelectService
    {
        private IRecipe _recipe;

        public IRecipe GetSelectedRecipe()
        {
            if (_recipe is null)
                throw new System.Exception("No recipe selected!");

            return _recipe;
        }

        public void SetSelectedRecipe(IRecipe recipe)
        {
            _recipe = recipe;
        }

        public void ClearSelectedRecipe()
        {
            _recipe = null;
        }
    }
}
