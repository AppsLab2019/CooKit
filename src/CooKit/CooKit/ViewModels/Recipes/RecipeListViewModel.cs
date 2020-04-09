using System;
using System.Collections.Generic;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Recipes;
using CooKit.Services.Repositories.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeListViewModel : BaseViewModel
    {
        public IEnumerable<Recipe> Recipes { get; private set; }
        public ICommand RefreshCommand { get; }
        public ICommand SelectCommand { get; }

        private readonly IRecipeRepository _repository;
        private readonly IRecipeSelectService _selectService;

        public RecipeListViewModel(IRecipeRepository repository,
            IRecipeSelectService selectService)
        {
            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            if (selectService is null)
                throw new ArgumentNullException(nameof(selectService));

            _repository = repository;
            _selectService = selectService;
                    
            RefreshCommand = new Command(RefreshRecipes);
            SelectCommand = new Command<Recipe>(SelectRecipe);

            Recipes = new []{ new Recipe
            {
                Name = "Test Recipe",
                PreviewImage = "https://blogs.biomedcentral.com/on-medicine/wp-content/uploads/sites/6/2019/09/iStock-1131794876.t5d482e40.m800.xtDADj9SvTVFjzuNeGuNUUGY4tm5d6UGU5tkKM0s3iPk-620x342.jpg"
            }};
        }

        private void RefreshRecipes()
        {
            //Recipes = await _repository.GetAllEntries();
            RaisePropertyChanged(nameof(Recipes));
        }

        private async void SelectRecipe(Recipe recipe)
        {
            _selectService.SetSelectedRecipe(recipe);
            await Shell.Current.GoToAsync("recipes/all/recipe");
        }
    }
}
