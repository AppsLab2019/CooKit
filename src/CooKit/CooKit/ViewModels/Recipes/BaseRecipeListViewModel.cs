using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public abstract class BaseRecipeListViewModel : ViewModel
    {
        public IEnumerable<IRecipe> Recipes { get; private set; }
        public ICommand SelectCommand => new Command<IRecipe>(async recipe => await Select(recipe));

        protected abstract Task<IEnumerable<IRecipe>> GetRecipes();

        public override async Task InitializeAsync(object parameter)
        {
            IsBusy = true;

            Recipes = await GetRecipes();
            RaisePropertyChanged(nameof(Recipes));

            IsBusy = false;
        }

        private Task Select(IRecipe recipe)
        {
            if (recipe is null)
                return Task.CompletedTask;

            return NavigationService.PushAsync<RecipeViewModel>(recipe);
        }
    }
}
