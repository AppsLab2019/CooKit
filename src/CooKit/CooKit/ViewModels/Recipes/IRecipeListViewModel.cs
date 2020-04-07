using System.Collections.Generic;
using System.Windows.Input;
using CooKit.Models.Recipes;

namespace CooKit.ViewModels.Recipes
{
    public interface IRecipeListViewModel
    {
        IEnumerable<IRecipe> Recipes { get; }

        ICommand RefreshCommand { get; }
        ICommand SelectCommand { get; }
    }
}
