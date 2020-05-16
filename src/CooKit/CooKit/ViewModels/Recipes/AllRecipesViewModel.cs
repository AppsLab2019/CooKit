using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models.Recipes;
using CooKit.Services.Stores.Recipes;

namespace CooKit.ViewModels.Recipes
{
    public sealed class AllRecipesViewModel : BaseRecipeListViewModel
    {
        private readonly IRecipeStore _store;

        public AllRecipesViewModel(IRecipeStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        protected override async Task<IEnumerable<IRecipe>> GetRecipes()
        {
            return await _store.GetAll();
        }
    }
}
