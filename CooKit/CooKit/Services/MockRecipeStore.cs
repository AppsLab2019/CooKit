using CooKit.Models;

namespace CooKit.Services
{
    public class MockRecipeStore : IRecipeStore
    {
        public IRecipe GetRecipe(int index) => 
            MockRecipe.Example;

        public IRecipe[] GetRecipeRange(int startIndex, int length)
        {
            var arr = new IRecipe[length];

            for (var i = 0; i < length; i++)
                arr[i] = MockRecipe.Example;

            return arr;
        }
    }
}
