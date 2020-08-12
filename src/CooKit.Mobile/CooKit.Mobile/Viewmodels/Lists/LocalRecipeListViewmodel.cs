using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Models;
using CooKit.Mobile.Repositories.Recipes;
using CooKit.Mobile.Viewmodels.Recipes;
using Xamarin.Forms;

namespace CooKit.Mobile.Viewmodels.Lists
{
    public class LocalRecipeListViewmodel : ParameterlessBaseViewmodel
    {
        private readonly IRecipeRepository _recipeRepository;
        private IList<Recipe> _recipes;

        public IList<Recipe> Recipes
        {
            get => _recipes;
            private set => OnPropertyChanged(ref _recipes, value);
        }

        public ICommand SelectCommand => new Command<Recipe>(async recipe => await SelectRecipeAsync(recipe));

        public LocalRecipeListViewmodel(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        protected override async Task InitializeAsync()
        {
            Recipes = await _recipeRepository.GetAllRecipesAsync();
        }

        private Task SelectRecipeAsync(Recipe recipe)
        {
            return recipe != null 
                ? NavigationService.PushAsync<RecipeDetailViewmodel>(recipe) 
                : Task.CompletedTask;
        }
    }
}
