using CooKit.Models;
using CooKit.Services.Repositories.Recipes;

namespace CooKit.Services.Stores.Recipes
{
    public sealed class RecipeStore : RepositoryStore<Recipe>, IRecipeStore
    {
        public RecipeStore(IRecipeRepository repository) : base(repository)
        {
        }
    }
}
