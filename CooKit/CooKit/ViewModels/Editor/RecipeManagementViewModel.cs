using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services;
using CooKit.Views.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class RecipeManagementViewModel : BaseViewModel
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
            _recipeStore = ((App) Application.Current).RecipeStore;
            Recipes = new ObservableCollection<IRecipe>(_recipeStore.LoadedObjects);
            _selectedRecipe = null;

            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);

            _recipeStore.PropertyChanged += HandleStoreChange;
        }

        private static void HandleAdd() =>
            Shell.Current.Navigation.PushAsync(new RecipeDesignerView());

        private void HandleRemove()
        {
            if (SelectedRecipe is null)
                return;

            _recipeStore.Remove(SelectedRecipe.Id);
            SelectedRecipe = null;
        }

        private void HandleStoreChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender != _recipeStore)
                return;

            Recipes = new ObservableCollection<IRecipe>(_recipeStore.LoadedObjects);
        }
    }
}
