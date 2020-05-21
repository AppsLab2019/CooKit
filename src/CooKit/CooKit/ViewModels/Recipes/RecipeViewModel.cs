using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;
using CooKit.Services.Favorites;
using CooKit.Services.Stores.Recipes;
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
        public IEnumerable<IStep> Steps { get; set; }

        #endregion

        public ICommand ToggleFavoriteCommand { get; }
        public ICommand SelectPictogramCommand { get; }

        private readonly IFavoriteService _favoriteService;
        private IRecipe _recipe;

        public RecipeViewModel(IFavoriteService favoriteService)
        {
            if (favoriteService is null)
                throw new ArgumentNullException(nameof(favoriteService));

            _favoriteService = favoriteService;

            ToggleFavoriteCommand = new Command(HandleToggleFavorite);
            SelectPictogramCommand = new Command<IPictogram>(HandlePictogramSelect);
        }

        public override Task InitializeAsync(object parameter)
        {
            _recipe = parameter as IRecipe;

            if (_recipe is null)
                throw new ArgumentException(nameof(parameter));

            var messageTask = MessageBroker.Send(this, "View", _recipe);

            Name = _recipe.Name;
            Description = _recipe.Description;
            EstimatedTime = _recipe.EstimatedTime;
            IsFavorite = _recipe.IsFavorite;

            Images = _recipe.Images;

            Ingredients = _recipe.Ingredients;
            Pictograms = _recipe.Pictograms;
            Steps = _recipe.Steps;

            RaiseAllPropertiesChanged();
            return messageTask;
        }

        private async void HandleToggleFavorite()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            IsFavorite = await _favoriteService.ToggleFavorite(_recipe);
            RaisePropertyChanged(nameof(IsFavorite));
            await SnackbarService.SnackbarAsync("Toggled favorite status!", 2750);

            IsBusy = false;
        }

        private async void HandlePictogramSelect(IPictogram pictogram)
        {
            if (pictogram is null)
                return;

            await AlertService.DisplayAlert(pictogram.Name, pictogram.Description, "Close");
        }
    }
}
