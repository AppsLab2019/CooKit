using System;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Recipes;
using CooKit.Services.Stores;

namespace CooKit.Services.Features
{
    // TODO: implement this
    public sealed class FeatureService : IFeatureService
    {
        private readonly IStore<IRecipe> _store;

        public FeatureService(IStore<IRecipe> store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        public async Task<IRecipe> GetFeaturedRecipe()
        {
            var recipes = await _store.GetAll();
            return recipes.FirstOrDefault();
        }
    }
}
