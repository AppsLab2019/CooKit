using CooKit.Models.Recipes;
using JetBrains.Annotations;

namespace CooKit.Services
{
    public class MockRecipeStore : IRecipeStore
    {

        [NotNull]
        public IRecipe GetRecipe(int index) => new MockRecipe();

        [NotNull]
        [ItemNotNull]
        public IRecipe[] GetRecipeRange(int startIndex, int length)
        {
            var arr = new IRecipe[length];

            for (var i = 0; i < length; i++)
                arr[i] = new MockRecipe();

            return arr;
        }

    }
}
