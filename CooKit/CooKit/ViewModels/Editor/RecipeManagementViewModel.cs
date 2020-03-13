using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services;
using CooKit.Views.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class RecipeManagementViewModel : BaseViewModel, IDisposable
    {
        public ObservableCollection<IRecipe> Recipes
        {
            get => _recipes;
            private set => HandlePropertyChange(ref _recipes, value);
        }
        private ObservableCollection<IRecipe> _recipes;

        public IRecipe SelectedRecipe
        {
            get => _selectedRecipe;
            set => HandlePropertyChange(ref _selectedRecipe, value);
        }
        private IRecipe _selectedRecipe;

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private readonly IRecipeStore _recipeStore;

        public RecipeManagementViewModel()
        {
            _recipeStore = App.GetRecipeStore();
            _recipes = new ObservableCollection<IRecipe>(_recipeStore.LoadedObjects);
            _selectedRecipe = null;

            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);

            _recipeStore.PropertyChanged += HandleStoreContentChange;
        }

        private static async void HandleAdd() =>
            await Shell.Current.Navigation.PushAsync(new RecipeDesignerView());

        private async void HandleRemove()
        {
            if (SelectedRecipe is null)
                return;

            await _recipeStore.RemoveAsync(SelectedRecipe.Id);
            SelectedRecipe = null;
        }

        private void HandleStoreContentChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender != _recipeStore)
                return;

            SelectedRecipe = null;
            Recipes = new ObservableCollection<IRecipe>(_recipeStore.LoadedObjects);
        }

        public void Dispose() =>
            _recipeStore.PropertyChanged -= HandleStoreContentChange;
    }
}
