using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models.Recipes;

namespace CooKit.Services.Favorites
{
    public interface IFavoriteService
    {
        Task<bool> ToggleFavorite(IRecipe recipe);
        Task<IList<IRecipe>> GetFavoriteRecipes();
    }
}
