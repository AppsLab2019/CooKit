using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public sealed class RecipeViewModel : BaseViewModel
    {
        public string RecipeName => _recipe.Name;
        public string RecipeDescription => _recipe.Description;
        public ImageSource RecipeImage => _recipe.Image;
        public TimeSpan RecipeRequiredTime => _recipe.RequiredTime;
        public ObservableCollection<IIngredient> RecipeIngredients { get; }
        public ObservableCollection<IPictogram> RecipePictograms { get; }
        public ObservableCollection<string> RecipeSteps { get; }

        public IPictogram SelectedPictogram
        {
            get => _selectedPictogram;
            set => HandlePropertyChange(ref _selectedPictogram, value);
        }
        private IPictogram _selectedPictogram;

        public ICommand PictogramSelectedCommand { get; }

        private readonly IRecipe _recipe;

        public RecipeViewModel(IRecipe recipe)
        {
            _recipe = recipe;

            RecipeIngredients = new ObservableCollection<IIngredient>(_recipe.Ingredients);
            RecipePictograms = new ObservableCollection<IPictogram>(_recipe.Pictograms);
            RecipeSteps = new ObservableCollection<string>(_recipe.Steps);

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
