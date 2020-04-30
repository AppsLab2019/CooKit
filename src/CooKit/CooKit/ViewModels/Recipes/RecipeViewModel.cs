using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
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

        public ICommand BackCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand SelectPictogramCommand { get; }

        private readonly IIngredientStore _ingredientStore;
        private readonly IPictogramStore _pictogramStore;

        public RecipeViewModel(IIngredientStore ingredientStore, IPictogramStore pictogramStore)
        {
            if (ingredientStore is null)
                throw new ArgumentNullException(nameof(ingredientStore));

            if (pictogramStore is null)
                throw new ArgumentNullException(nameof(pictogramStore));

            _ingredientStore = ingredientStore;
            _pictogramStore = pictogramStore;

            BackCommand = new Command(HandleBack);
            ToggleFavoriteCommand = new Command(HandleToggleFavorite);
            SelectPictogramCommand = new Command<IPictogram>(HandlePictogramSelect);
        }

        public override async Task InitializeAsync(object parameter)
        {
            if (!(parameter is IRecipe recipe))
                throw new ArgumentException(nameof(parameter));

            IsBusy = true;

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

            IsBusy = false;
            RaiseAllPropertiesChanged();
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
