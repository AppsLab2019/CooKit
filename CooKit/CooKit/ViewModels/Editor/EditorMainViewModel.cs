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

        public EditorMainViewModel()
        {
            ManageIngredientsCommand = new Command(() => PushAsync(new IngredientManagementView()));
            ManagePictogramsCommand = new Command(() => PushAsync(new PictogramManagementView()));
            ManageRecipesCommand = new Command(() => PushAsync(new RecipeManagementView()));
        }
    }
}
