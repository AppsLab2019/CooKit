using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Extensions;
using CooKit.Models.Recipes;
using CooKit.Services.Stores.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorStartViewModel : ViewModel
    {
        private readonly IRecipeStore _store;

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public IRecipe SelectedRecipe { get; set; }
        public ObservableCollection<IRecipe> Recipes { get; private set; }

        public EditorStartViewModel(IRecipeStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;

            AddCommand = new Command(async () => await HandleAdd());
            EditCommand = new Command(async () => await HandleEdit());
            DeleteCommand = new Command(async () => await HandleDelete());
        }

        public override async Task InitializeAsync(object parameter)
        {
            IsBusy = true;

            var recipes = await _store.GetAll();
            Recipes = recipes.ToObservableCollection();
            SelectedRecipe = null;

            IsBusy = false;
            RaiseAllPropertiesChanged();
        }

        private Task HandleAdd()
        {
            return NavigationService.PushAsync<EditorMainViewModel>();
        }

        private async Task HandleEdit()
        {
            if (SelectedRecipe is null)
                return;

            await NavigationService.PushAsync<EditorMainViewModel>(SelectedRecipe);
        }

        private Task HandleDelete()
        {
            if (SelectedRecipe is null)
                return Task.CompletedTask;

            throw new NotImplementedException();
        }
    }
}
