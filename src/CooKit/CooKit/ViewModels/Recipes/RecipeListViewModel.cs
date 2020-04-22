using System;
using System.Collections.Generic;
using System.Windows.Input;
using CooKit.Models.Recipes;
using CooKit.Services.Recipes;
using CooKit.Services.Stores.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeListViewModel : BaseViewModel
    {
        public IEnumerable<IRecipe> Recipes { get; private set; }
        public ICommand RefreshCommand { get; }
        public ICommand SelectCommand { get; }

        private readonly IRecipeStore _store;
        private readonly IRecipeSelectService _selectService;

        public RecipeListViewModel(IRecipeStore store, IRecipeSelectService selectService)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            if (selectService is null)
                throw new ArgumentNullException(nameof(selectService));

            _store = store;
            _selectService = selectService;
                    
            RefreshCommand = new Command(RefreshRecipes);
            SelectCommand = new Command<Recipe>(SelectRecipe);
        }

        private async void RefreshRecipes()
        {
            Recipes = await _store.GetAll();
            RaisePropertyChanged(nameof(Recipes));
        }

        private async void SelectRecipe(Recipe recipe)
        {
            _selectService.SetSelectedRecipe(recipe);
            await Shell.Current.GoToAsync("recipes/all/recipe");
        }
    }
}
