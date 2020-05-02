using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Recipes;
using CooKit.Services.Stores.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeListViewModel : ViewModel
    {
        public IEnumerable<IRecipe> Recipes { get; private set; }
        public ICommand SelectCommand { get; }

        private readonly IRecipeStore _store;

        public RecipeListViewModel(IRecipeStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
            SelectCommand = new Command<IRecipe>(async recipe => await SelectRecipe(recipe));
        }

        public override async Task InitializeAsync(object parameter)
        {
            IsBusy = true;
            Recipes = await _store.GetAll();

            IsBusy = false;
            RaisePropertyChanged(nameof(Recipes));
        }

        private Task SelectRecipe(IRecipe recipe)
        {
            return NavigationService.PushAsync<RecipeViewModel>(recipe);
        }
    }
}
