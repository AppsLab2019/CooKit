using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;
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

        private readonly IRecipeStore _store;
        private IRecipe _recipe;

        public RecipeViewModel(IRecipeStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;

            ToggleFavoriteCommand = new Command(HandleToggleFavorite);
            SelectPictogramCommand = new Command<IPictogram>(HandlePictogramSelect);
        }

        public override Task InitializeAsync(object parameter)
        {
            _recipe = parameter as IRecipe;

            if (_recipe is null)
                throw new ArgumentException(nameof(parameter));

            Name = _recipe.Name;
            Description = _recipe.Description;
            EstimatedTime = _recipe.EstimatedTime;
            IsFavorite = _recipe.IsFavorite;

            Images = _recipe.Images;

            Ingredients = _recipe.Ingredients;
            Pictograms = _recipe.Pictograms;
            Steps = _recipe.Steps;

            RaiseAllPropertiesChanged();

            // TODO: replace me
            return MessageBroker.Send(this, "View", _recipe);
        }

        private async void HandleToggleFavorite()
        {
            IsFavorite = !IsFavorite;
            _recipe.IsFavorite = IsFavorite;

            RaisePropertyChanged(nameof(IsFavorite));

            await _store.Update(_recipe);
            await SnackbarService.SnackbarAsync("Toggled favorite status!", 2750);
        }

        private async void HandlePictogramSelect(IPictogram pictogram)
        {
            if (pictogram is null)
                return;

            await AlertService.DisplayAlert(pictogram.Name, pictogram.Description, "Close");
        }
    }
}
