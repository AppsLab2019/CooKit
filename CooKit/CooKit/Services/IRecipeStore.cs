using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using CooKit.Models;

namespace CooKit.Services
{
    public interface IRecipeStore : INotifyPropertyChanged
    {
        IReadOnlyList<IRecipe> LoadedRecipes { get; }

        IRecipeBuilder CreateRecipeBuilder();

        IRecipe LoadRecipe();
        Task<IRecipe> LoadRecipeAsync();

        IRecipe LoadRecipe(Guid id);
        Task<IRecipe> LoadRecipeAsync(Guid id);

        void AddRecipe(IRecipeBuilder recipeBuilder);
        Task AddRecipeAsync(IRecipeBuilder recipeBuilder);

        bool RemoveRecipe(Guid id);
        Task<bool> RemoveRecipeAsync(Guid id);
    }
}
