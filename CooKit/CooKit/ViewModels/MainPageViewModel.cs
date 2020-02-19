using CooKit.Services;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Impl;
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
            _recipeStore = new MockRecipeStore();

            _isBusy = false;
            Recipes = new ObservableCollection<IRecipe>();

            LoadRecipes();
            ThresholdReachedCommand = new Command(LoadRecipes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private async void LoadRecipes()
        {
            if (_isBusy)
                return;

            _isBusy = true;

            var recipes = await _recipeStore.LoadRecipesAsync(10);
            
            foreach (var recipe in recipes)
                Recipes.Add(recipe);

            _isBusy = false;
        }
    }
}
