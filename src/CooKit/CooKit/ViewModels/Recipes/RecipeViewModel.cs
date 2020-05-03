using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeViewModel : ViewModel
    {
        #region Properties
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int EstimatedTime { get; private set; }
        public bool IsFavorite { get; private set; }
        public IEnumerable<string> Images { get; private set; }
        public IEnumerable<IPictogram> Pictograms { get; private set; }
        public IEnumerable<IIngredient> Ingredients { get; private set; }

        #endregion

        public ICommand BackCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand SelectPictogramCommand { get; }

        public RecipeViewModel()
        {
            BackCommand = new Command(HandleBack);
            ToggleFavoriteCommand = new Command(HandleToggleFavorite);
            SelectPictogramCommand = new Command<IPictogram>(HandlePictogramSelect);
        }

        public override Task InitializeAsync(object parameter)
        {
            if (!(parameter is IRecipe recipe))
                throw new ArgumentException(nameof(parameter));

            IsBusy = true;

            // TODO: move mapping to a separate function
            Name = recipe.Name;
            Description = recipe.Description;
            EstimatedTime = recipe.EstimatedTime;
            IsFavorite = recipe.IsFavorite;
            //Images = recipe.Images;

            Ingredients = recipe.Ingredients;
            Pictograms = recipe.Pictograms;

            IsBusy = false;
            RaiseAllPropertiesChanged();
            return Task.CompletedTask;
        }

        private async void HandleBack()
        {
            await NavigationService.PopAsync();
        }

        private async void HandleToggleFavorite()
        {
            IsFavorite = !IsFavorite;
            RaisePropertyChanged(nameof(IsFavorite));
            await AlertService.DisplayAlert("Not Finished!", "Title", "lmao");
        }

        private async void HandlePictogramSelect(IPictogram pictogram)
        {
            if (pictogram is null)
                return;

            await AlertService.DisplayAlert(pictogram.Name, pictogram.Description, "Close");
        }
    }
}
