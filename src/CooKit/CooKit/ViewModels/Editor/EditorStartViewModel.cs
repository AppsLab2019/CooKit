using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Extensions;
using CooKit.Models.Recipes;
using CooKit.Services.Stores;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorStartViewModel : ViewModel
    {
        private readonly IStore<IRecipe> _store;

        public EditorStartViewModel(IStore<IRecipe> store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        public override async Task InitializeAsync()
        {
            IsBusy = true;

            var recipes = await _store.GetAll();
            Recipes = recipes.ToObservableCollection();

            IsBusy = false;
        }

        private Task AddRecipe()
        {
            return NavigationService.PushAsync<EditorMainViewModel>();
        }

        private Task EditRecipe(IRecipe recipe)
        {
            if (recipe is null)
                return Task.CompletedTask;

            return NavigationService.PushAsync<EditorMainViewModel>(recipe);
        }

        private async Task RemoveRecipe(IRecipe recipe)
        {
            if (recipe is null)
                return;

            Recipes.Remove(recipe);
            // TODO: remove from store

            await SnackbarService.SnackbarAsync("Recipe removed!", 2750);
        }

        public ICommand AddCommand => new Command(async () => await AddRecipe());
        public ICommand EditCommand => new Command<IRecipe>(async recipe => await EditRecipe(recipe));
        public ICommand RemoveCommand => new Command<IRecipe>(async recipe => await RemoveRecipe(recipe));

        public ObservableCollection<IRecipe> Recipes
        {
            get => _recipes;
            set => OnPropertyChanged(ref _recipes, value);
        }

        private ObservableCollection<IRecipe> _recipes;
    }
}
