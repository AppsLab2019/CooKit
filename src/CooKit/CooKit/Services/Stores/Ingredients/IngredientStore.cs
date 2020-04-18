using CooKit.Models;
using CooKit.Services.Repositories.Ingredients;

namespace CooKit.Services.Stores.Ingredients
{
    public sealed class IngredientStore : RepositoryStore<Ingredient>, IIngredientStore
    {
        public IngredientStore(IIngredientRepository repository) : base(repository)
        {
        }
    }
}
