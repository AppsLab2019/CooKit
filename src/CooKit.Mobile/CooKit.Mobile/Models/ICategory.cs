using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Providers.Recipes;

namespace CooKit.Mobile.Models
{
    public interface ICategory
    {
        string Name { get; }
        Task<IList<Recipe>> GetPreviewRecipesAsync();
        IRecipeListProvider GetRecipeListProvider();
    }
}
