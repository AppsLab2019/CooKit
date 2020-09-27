using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Models;
using Xamarin.Forms;

namespace CooKit.Mobile.Viewmodels
{
    public class RecipeListViewmodel : ParameterBaseViewmodel<ICategory>
    {
        public ICommand OpenRecipeCommand => new Command<Recipe>(async recipe => await OpenRecipeAsync(recipe));

        public string Title
        {
            get => _title;
            set => OnPropertyChanged(ref _title, value);
        }

        public IList<Recipe> Recipes
        {
            get => _recipes;
            set => OnPropertyChanged(ref _recipes, value);
        }

        private string _title;
        private IList<Recipe> _recipes;

        protected override async Task InitializeAsync(ICategory category)
        {
            Title = category.Name;

            var provider = category.GetRecipeListProvider();
            Recipes = await provider.GetRecipesAsync();
        }

        private Task OpenRecipeAsync(Recipe recipe)
        {
            return recipe != null
                ? NavigationService.PushAsync<RecipeDetailViewmodel>(recipe)
                : Task.CompletedTask;
        }
    }
}
