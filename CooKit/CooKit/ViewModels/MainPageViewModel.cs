using CooKit.Models.Recipes;
using CooKit.Services;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public class MainPageViewModel
    {

        public readonly IRecipeStore RecipeStore;
        public ObservableCollection<IRecipe> Recipes { get; }
        public ICommand ThresholdReachedCommand { get; }

        private int _currentIndex;

        public MainPageViewModel()
        {
            RecipeStore = new MockRecipeStore();
            Recipes = new ObservableCollection<IRecipe>();
            _currentIndex = 0;

            LoadRecipes();
            ThresholdReachedCommand = new Command(LoadRecipes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LoadRecipes()
        {
            foreach (var recipe in RecipeStore.GetRecipeRange(_currentIndex, 10))
                Recipes.Add(recipe);

            _currentIndex += 10;
        }

    }
}
