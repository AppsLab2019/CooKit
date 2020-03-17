using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public sealed class RecipeViewModel : BaseViewModel
    {
        public IRecipe Recipe { get; }

        public IPictogram SelectedPictogram
        {
            get => _selectedPictogram;
            set => HandlePropertyChange(ref _selectedPictogram, value);
        }
        private IPictogram _selectedPictogram;

        public ICommand PictogramSelectedCommand { get; }

        public RecipeViewModel(IRecipe recipe)
        {
            Recipe = recipe;
            PictogramSelectedCommand = new Command(HandlePictogramSelected);
        }

        private async void HandlePictogramSelected()
        {
            if (SelectedPictogram is null)
                return;

            await DisplayAlert(SelectedPictogram.Name, SelectedPictogram.Description, "Ok");

            SelectedPictogram = null;
        }
    }
}
