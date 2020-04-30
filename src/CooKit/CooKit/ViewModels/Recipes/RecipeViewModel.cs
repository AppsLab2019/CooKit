using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Services.Alerts;
using CooKit.Services.Recipes;
using CooKit.Services.Stores.Ingredients;
using CooKit.Services.Stores.Pictograms;
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

        public ICommand UpdateCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand SelectPictogramCommand { get; }

        private readonly IRecipeSelectService _selectService;
        private readonly IAlertService _alertService;

        private readonly IIngredientStore _ingredientStore;
        private readonly IPictogramStore _pictogramStore;

        public RecipeViewModel(IRecipeSelectService selectService, IAlertService alertService,
            IIngredientStore ingredientStore, IPictogramStore pictogramStore)
        {
            if (selectService is null)
                throw new ArgumentNullException(nameof(selectService));

            if (alertService is null)
                throw new ArgumentNullException(nameof(alertService));

            if (ingredientStore is null)
                throw new ArgumentNullException(nameof(ingredientStore));

            if (pictogramStore is null)
                throw new ArgumentNullException(nameof(pictogramStore));

            _selectService = selectService;
            _alertService = alertService;

            _ingredientStore = ingredientStore;
            _pictogramStore = pictogramStore;

            UpdateCommand = new Command(HandleUpdate);
            BackCommand = new Command(HandleBack);
            ToggleFavoriteCommand = new Command(HandleToggleFavorite);
            SelectPictogramCommand = new Command<IPictogram>(HandlePictogramSelect);
        }

        private async void HandleUpdate()
        {
            var recipe = _selectService.GetSelectedRecipe();
            var pictogramTask = _pictogramStore.GetByIds(recipe.PictogramIds);
            var ingredientTask = _ingredientStore.GetByIds(recipe.IngredientIds);

            Name = recipe.Name;
            Description = recipe.Description;
            EstimatedTime = recipe.EstimatedTime;
            IsFavorite = recipe.IsFavorite;
            //Images = recipe.Images;

            await Task.WhenAll(pictogramTask, ingredientTask);

            Pictograms = pictogramTask.Result;
            Ingredients = ingredientTask.Result;

            RaiseAllPropertiesChanged();
        }

        private async void HandleBack()
        {
            _selectService.ClearSelectedRecipe();
            await Shell.Current.Navigation.PopAsync();
        }

        private async void HandleToggleFavorite()
        {
            IsFavorite = !IsFavorite;
            RaisePropertyChanged(nameof(IsFavorite));
            await _alertService.DisplayAlert("Not Finished!", "Title", "lmao");
        }

        private async void HandlePictogramSelect(IPictogram pictogram)
        {
            if (pictogram is null)
                return;

            await _alertService.DisplayAlert(pictogram.Name, pictogram.Description, "Close");
        }
    }
}
