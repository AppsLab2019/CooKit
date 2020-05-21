using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CooKit.Models.Recipes;
using CooKit.Services.Messages;
using CooKit.Services.Stores.Recipes;

namespace CooKit.Services.Favorites
{
    public sealed class FavoriteService : IFavoriteService
    {
        private readonly IRecipeStore _store;
        private readonly IMessageBroker _broker;

        public FavoriteService(IRecipeStore store, IMessageBroker broker)
        {
            if (store is null) 
                throw new ArgumentNullException(nameof(store));

            if (broker is null) 
                throw new ArgumentNullException(nameof(broker));

            _store = store;
            _broker = broker;
        }

        public async Task<bool> ToggleFavorite(IRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            var state = !recipe.IsFavorite;
            recipe.IsFavorite = state;

            var updateTask = _store.Update(recipe);
            var messageTask = _broker.Send(this, "Toggled Favorite", recipe);

            await Task.WhenAll(updateTask, messageTask);

            return state;
        }

        public async Task<IList<IRecipe>> GetFavoriteRecipes()
        {
            var recipes = await _store.GetAll();

            return recipes
                .Where(recipe => recipe.IsFavorite)
                .ToList();
        }
    }
}
