using System;
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

        IRecipe LoadRecipe(Guid id);
        Task<IRecipe> LoadRecipeAsync(Guid id);
    }
}
