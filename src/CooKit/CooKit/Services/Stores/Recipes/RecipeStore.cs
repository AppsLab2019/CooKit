using CooKit.Models.Recipes;
using CooKit.Repositories.Recipes;

namespace CooKit.Services.Stores.Recipes
{
    public sealed class RecipeStore : RepositoryStore<IRecipe>, IRecipeStore
    {
        public RecipeStore(IRecipeRepository repository) : base(repository)
        {
        }
    }
}
