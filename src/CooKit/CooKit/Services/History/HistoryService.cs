using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models.Recipes;
using CooKit.Services.Stores.Recipes;

namespace CooKit.Services.History
{
    // TODO: implement this
    public sealed class HistoryService : IHistoryService
    {
        private readonly IRecipeStore _store;

        public HistoryService(IRecipeStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        public Task<IList<IRecipe>> GetLastVisitedRecipes()
        {
            return _store.GetAll();
        }
    }
}
