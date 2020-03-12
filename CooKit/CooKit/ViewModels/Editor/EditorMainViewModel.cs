using System.Windows.Input;
using CooKit.Views.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel
    {
        public ICommand ManageIngredientsCommand { get; }
        public ICommand ManagePictogramsCommand { get; }
        public ICommand ManageRecipesCommand { get; }

        public EditorMainViewModel()
        {
            var navigation = Shell.Current.Navigation;

            ManageIngredientsCommand = new Command(() => navigation.PushAsync(new IngredientManagementView()));
            ManagePictogramsCommand = new Command(() => navigation.PushAsync(new PictogramManagementView()));
            ManageRecipesCommand = new Command(() => navigation.PushAsync(new RecipeManagementView()));
        }
    }
}
