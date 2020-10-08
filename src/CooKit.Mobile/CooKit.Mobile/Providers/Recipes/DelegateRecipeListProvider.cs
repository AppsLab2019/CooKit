using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Providers.Recipes
{
    public class DelegateRecipeListProvider : IRecipeListProvider
    {
        private readonly RecipeListAsyncDelegate _delegate;

        public DelegateRecipeListProvider(RecipeListAsyncDelegate @delegate)
        {
            _delegate = @delegate;
        }

        public Task<IList<Recipe>> GetRecipesAsync()
        {
            return _delegate != null 
                ? _delegate.Invoke()
                : Task.FromResult((IList<Recipe>) null);
        }
    }
}
