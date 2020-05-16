using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Recipes;
using CooKit.Services.Stores.Recipes;

namespace CooKit.ViewModels.Recipes
{
    public sealed class FavoriteRecipesViewModel : BaseRecipeListViewModel
    {
        private readonly IRecipeStore _store;

        public FavoriteRecipesViewModel(IRecipeStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        protected override async Task<IEnumerable<IRecipe>> GetRecipes()
        {
            var recipes = await _store.GetAll();

            return recipes
                .Where(recipe => recipe.IsFavorite)
                .ToList();
        }
    }
}
