using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services
{
    public interface IRecipeStore
    {
        IReadOnlyList<IRecipe> LoadedRecipes { get; }

        IRecipe LoadRecipe();
        Task<IRecipe> LoadRecipeAsync();

        IReadOnlyList<IRecipe> LoadRecipes(int count);
        Task<IReadOnlyList<IRecipe>> LoadRecipesAsync(int count);
    }
}
