using CooKit.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

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

            Recipes = new ObservableCollection<IRecipe>(_recipeStore.LoadedObjects);

            LoadRecipes();
            ThresholdReachedCommand = new Command(LoadRecipes);
        }

        private async void LoadRecipes()
        {
            if (_isBusy)
                return;

            _isBusy = true;

            for (var i = 0; i < 10; i++)
            {
                var recipe = await _recipeStore.LoadNextAsync();

                if (recipe is null)
                    break;

                Recipes.Add(recipe);
            }

            _isBusy = false;
        }
    }
}
