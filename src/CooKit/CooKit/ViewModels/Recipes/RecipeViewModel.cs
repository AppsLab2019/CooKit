using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Recipes;
using CooKit.Services.Repositories.Ingredients;
using CooKit.Services.Repositories.Pictograms;
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
        private readonly IPictogramRepository _pictogramRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeViewModel(IRecipeSelectService selectService,
            IPictogramRepository pictogramRepository,
            IIngredientRepository ingredientRepository)
        {
            if (selectService is null)
                throw new ArgumentNullException(nameof(selectService));

            if (pictogramRepository is null)
                throw new ArgumentNullException(nameof(pictogramRepository));

            if (ingredientRepository is null)
                throw new ArgumentNullException(nameof(ingredientRepository));

            _selectService = selectService;
            _pictogramRepository = pictogramRepository;
            _ingredientRepository = ingredientRepository;

            UpdateCommand = new Command(HandleUpdate);
        }

        private async void HandleUpdate()
        {
            var recipe = _selectService.GetSelectedRecipe();
            var pictogramTask = _pictogramRepository.GetByIds(recipe.PictogramIds);
            var ingredientTask = _ingredientRepository.GetByIds(recipe.IngredientIds);

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
