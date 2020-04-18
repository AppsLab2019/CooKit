using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Recipes;
using CooKit.Services.Stores.Ingredients;
using CooKit.Services.Stores.Pictograms;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeViewModel : BaseViewModel
    {
        #region Properties
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int EstimatedTime { get; private set; }
        public bool IsFavorite { get; private set; }
        public IEnumerable<string> Images { get; private set; }
        public IEnumerable<Pictogram> Pictograms { get; private set; }
        public IEnumerable<Ingredient> Ingredients { get; private set; }

        #endregion

        public ICommand UpdateCommand { get; }

        private readonly IRecipeSelectService _selectService;
        private readonly IIngredientStore _ingredientStore;
        private readonly IPictogramStore _pictogramStore;

        public RecipeViewModel(IRecipeSelectService selectService, IIngredientStore ingredientStore, IPictogramStore pictogramStore)
        {
            if (selectService is null)
                throw new ArgumentNullException(nameof(selectService));

            if (ingredientStore is null)
                throw new ArgumentNullException(nameof(ingredientStore));

            if (pictogramStore is null)
                throw new ArgumentNullException(nameof(pictogramStore));

            _selectService = selectService;
            _ingredientStore = ingredientStore;
            _pictogramStore = pictogramStore;

            UpdateCommand = new Command(HandleUpdate);
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
    }
}
