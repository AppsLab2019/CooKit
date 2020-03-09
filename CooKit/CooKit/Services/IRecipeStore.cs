using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services
{
    public interface IRecipeStore
    {
        IReadOnlyList<IRecipe> LoadedRecipes { get; }

        IRecipe GetNextRecipe();
        Task<IRecipe> GetNextRecipeAsync();
    }
}
