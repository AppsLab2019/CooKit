using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Models;

namespace CooKit.Mobile.Repositories.Recipes
{
    public interface IRecipeRepository
    {
        ValueTask<Recipe> GetRecipeAsync(int id);
        ValueTask<IList<Recipe>> GetAllRecipesAsync();
    }
}
