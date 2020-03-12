using System.Collections.Generic;
using CooKit.Models;

namespace CooKit.Services
{
    public interface IIngredientStore
    {
        IReadOnlyList<IIngredient> LoadedIngredients { get; }
    }
}
