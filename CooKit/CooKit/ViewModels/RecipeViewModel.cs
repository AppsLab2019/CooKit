using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public sealed class RecipeViewModel : BaseViewModel
    {
        public IRecipe Recipe { get; }

        public ICommand PictogramSelectedCommand { get; }

        public RecipeViewModel(IRecipe recipe)
        {
            Recipe = recipe;
            PictogramSelectedCommand = new Command<IPictogram>(HandlePictogramSelected);
        }

        private async void HandlePictogramSelected(IPictogram pictogram)
        {
            if (pictogram is null)
                return;

            await DisplayAlert(pictogram.Name, pictogram.Description, "Ok");
        }
    }
}
