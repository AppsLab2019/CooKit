using CooKit.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;
using CooKit.Models.Impl;

namespace CooKit.ViewModels
{
    public class MainPageViewModel
    {
        private bool _isBusy;
        private readonly IRecipeStore _recipeStore;

        public ObservableCollection<IRecipe> Recipes { get; }

        public ICommand ThresholdReachedCommand { get; }

        public MainPageViewModel()
        {
            _isBusy = false;
            _recipeStore = ((App) Application.Current).RecipeStore;

            Recipes = new ObservableCollection<IRecipe>();

            LoadRecipes();
            ThresholdReachedCommand = new Command(LoadRecipes);
        }

        private async void LoadRecipes()
        {
            if (_isBusy)
                return;

            _isBusy = true;

            for (var i = 0; i < 10; i++)
                Recipes.Add(await _recipeStore.GetNextRecipeAsync() ?? MockRecipe.Example);

            _isBusy = false;
        }
    }
}
