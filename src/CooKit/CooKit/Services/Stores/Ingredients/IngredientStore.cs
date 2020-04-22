using CooKit.Models.Ingredients;
using CooKit.Services.Repositories.Ingredients;

namespace CooKit.Services.Stores.Ingredients
{
    public sealed class IngredientStore : RepositoryStore<IIngredient>, IIngredientStore
    {
        public IngredientStore(IIngredientRepository repository) : base(repository)
        {
        }
    }
}
