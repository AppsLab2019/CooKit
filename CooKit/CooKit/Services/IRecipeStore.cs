using CooKit.Models;
using JetBrains.Annotations;

namespace CooKit.Services
{
    public interface IRecipeStore
    {

        [NotNull]
        IRecipe GetRecipe(int index);

        [NotNull]
        [ItemNotNull]
        IRecipe[] GetRecipeRange(int startIndex, int length);

    }
}
