using CooKit.Models.Ingredients;
using CooKit.Repositories.Ingredients;

namespace CooKit.Services.Stores.Ingredients
{
    public sealed class IngredientStore : RepositoryStore<IIngredient>, IIngredientStore
    {
        public IngredientStore(IIngredientRepository repository) : base(repository)
        {
        }
    }
}
