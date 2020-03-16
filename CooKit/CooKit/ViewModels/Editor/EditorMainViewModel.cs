using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Views.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : BaseViewModel
    {
        public ICommand ManageIngredientsCommand { get; }
        public ICommand ManagePictogramsCommand { get; }
        public ICommand ManageRecipesCommand { get; }
        public ICommand PurgeStepsCommand { get; }

        public EditorMainViewModel()
        {
            ManageIngredientsCommand = new Command(() => PushAsync(new IngredientManagementView()));
            ManagePictogramsCommand = new Command(() => PushAsync(new PictogramManagementView()));
            ManageRecipesCommand = new Command(() => PushAsync(new RecipeManagementView()));
            PurgeStepsCommand = new Command(HandlePurgeSteps);
        }

        public async void HandlePurgeSteps()
        {
            var result = await DisplayAlert("Confirmation", "Are you sure you want to purge all unused recipe steps?",
                "Yes", "No");

            if (!result)
                return;

            var loadedRecipes = App
                .GetRecipeStore()
                .LoadedObjects;

            var stepStore = App.GetRecipeStepStore();

            var loadedSteps = stepStore
                .LoadedObjects
                .ToList();

            foreach (var recipe in loadedRecipes)
                foreach (var step in recipe.Steps)
                    loadedSteps.Remove(step);

            var tasks = loadedSteps
                .Select(step => stepStore.RemoveAsync(step.Id))
                .ToArray();

            await Task.WhenAll(tasks);
            await DisplayAlert("Success", $"Successfully removed {tasks.Length} steps!", "Ok");
        }
    }
}
