using System.Collections.Generic;
using System.Windows.Input;
using CooKit.Models;

namespace CooKit.ViewModels.Recipes
{
    public interface IRecipeListViewModel
    {
        IEnumerable<Recipe> Recipes { get; }

        ICommand RefreshCommand { get; }
        ICommand SelectCommand { get; }
    }
}
