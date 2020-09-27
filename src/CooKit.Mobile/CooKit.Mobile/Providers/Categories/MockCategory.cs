using System.Collections.Generic;
using System.Threading.Tasks;
using CooKit.Mobile.Models;
using CooKit.Mobile.Providers.Recipes;

namespace CooKit.Mobile.Providers.Categories
{
    public class MockCategory : ICategory, IRecipeListProvider
    {
        public string Name { get; set; }
        public IList<Recipe> Recipes { get; set; }

        public MockCategory()
        {
        }

        public MockCategory(string name, IList<Recipe> recipes)
        {
            Name = name;
            Recipes = recipes;
        }

        public Task<IList<Recipe>> GetPreviewRecipesAsync()
        {
            return Task.FromResult(Recipes);
        }

        public IRecipeListProvider GetRecipeListProvider()
        {
            return this;
        }

        public Task<IList<Recipe>> GetRecipesAsync()
        {
            return Task.FromResult(Recipes);
        }
    }
}
