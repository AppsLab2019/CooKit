using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Repositories.Recipes;
using CooKit.Views.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeListViewModel : BaseViewModel
    {
        public IEnumerable<Recipe> Recipes { get; private set; }
        public ICommand RefreshCommand { get; }
        public ICommand SelectCommand { get; }

        private readonly IRecipeRepository _repository;

        public RecipeListViewModel(IRecipeRepository repository)
        {
            if (repository is null)
                throw new ArgumentNullException(nameof(repository));

            _repository = repository;
                    
            RefreshCommand = new Command(async () => await RefreshRecipes());
            SelectCommand = new Command<Recipe>(async recipe => await SelectRecipe(recipe));

            Recipes = new []{ new Recipe
            {
                Name = "Test Recipe",
                PreviewImage = "https://blogs.biomedcentral.com/on-medicine/wp-content/uploads/sites/6/2019/09/iStock-1131794876.t5d482e40.m800.xtDADj9SvTVFjzuNeGuNUUGY4tm5d6UGU5tkKM0s3iPk-620x342.jpg"
            }};
        }

        private Task RefreshRecipes()
        {
            //Recipes = await _repository.GetAllEntries();
            RaisePropertyChanged(nameof(Recipes));
            return Task.CompletedTask;
        }

        private async Task SelectRecipe(Recipe recipe)
        {
            await Shell.Current.Navigation.PushAsync(new RecipeView());
        }
    }
}
