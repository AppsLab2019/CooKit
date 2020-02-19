using System.Collections.Generic;
using System.Linq;
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

        public IRecipe LoadRecipe()
        {
            var recipe = MockRecipe.Example;

            _recipes.Add(recipe);
            return recipe;
        }

        public Task<IRecipe> LoadRecipeAsync() =>
            Task.FromResult(LoadRecipe());

        public IReadOnlyList<IRecipe> LoadRecipes(int count)
        {
            var recipes = Enumerable
                .Repeat(MockRecipe.Example, count)
                .ToArray();

            _recipes.AddRange(recipes);
            return recipes;
        }

        public Task<IReadOnlyList<IRecipe>> LoadRecipesAsync(int count) =>
            Task.FromResult(LoadRecipes(count));
    }
}
