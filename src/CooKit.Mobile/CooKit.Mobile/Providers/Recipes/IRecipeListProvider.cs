using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Providers.Recipes
{
    public interface IRecipeListProvider
    {
        Task<IList<Recipe>> GetRecipesAsync();
    }
}
