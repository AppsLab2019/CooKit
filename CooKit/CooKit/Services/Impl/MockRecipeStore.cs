using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Models;
using CooKit.Models.Impl;

namespace CooKit.Services.Impl
{
    public sealed class MockRecipeStore : IRecipeStore
    {
        private readonly List<IRecipe> _recipes;
        public IReadOnlyList<IRecipe> LoadedRecipes => _recipes;

        public MockRecipeStore() =>
            _recipes = new List<IRecipe>();

        public IRecipe GetNextRecipe()
        {
            var recipe = MockRecipe.Example;

            _recipes.Add(recipe);
            return recipe;
        }

        public Task<IRecipe> GetNextRecipeAsync() =>
            Task.FromResult(GetNextRecipe());
    }
}
