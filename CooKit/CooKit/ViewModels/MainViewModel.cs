using System;
using CooKit.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Views;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public class MainViewModel : BaseViewModel, IDisposable
    {
        public ObservableCollection<IRecipe> Recipes
        {
            get => _recipes;
            set => HandlePropertyChange(ref _recipes, value);
        }
        private ObservableCollection<IRecipe> _recipes;

        public IRecipe SelectedRecipe
        {
            get => _selectedRecipe;
            set => HandlePropertyChange(ref _selectedRecipe, value);
        }
        private IRecipe _selectedRecipe;

        public ICommand RecipeSelectCommand { get; }

        private readonly IRecipeStore _recipeStore;

        public MainViewModel()
        {
            _recipeStore = App.GetRecipeStore();

            Recipes = new ObservableCollection<IRecipe>(_recipeStore.LoadedObjects);
            RecipeSelectCommand = new Command(HandleRecipeSelect);

            _recipeStore.PropertyChanged += HandleStoreContentChange;
        }

        private async void HandleRecipeSelect()
        {
            if (SelectedRecipe is null)
                return;

            var recipePage = new RecipeView {BindingContext = new RecipeViewModel(SelectedRecipe)};
            await Shell.Current.Navigation.PushAsync(recipePage);

            SelectedRecipe = null;
        }

        private void HandleStoreContentChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender == _recipeStore)
                Recipes = new ObservableCollection<IRecipe>(_recipeStore.LoadedObjects);
        }

        public void Dispose() =>
            _recipeStore.PropertyChanged -= HandleStoreContentChange;
    }
}
