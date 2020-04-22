using CooKit.Models.Recipes;
using CooKit.Services.Repositories.Recipes;

namespace CooKit.Services.Stores.Recipes
{
    public sealed class RecipeStore : RepositoryStore<IRecipe>, IRecipeStore
    {
        public RecipeStore(IRecipeRepository repository) : base(repository)
        {
        }
    }
}
