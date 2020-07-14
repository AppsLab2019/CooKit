﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;
using CooKit.Services.Favorites;
using Xamarin.Forms;

namespace CooKit.ViewModels.Recipes
{
    public sealed class RecipeViewModel : ViewModel<IRecipe>
    {
        public ICommand ToggleFavoriteCommand => new Command(async () => await ToggleFavorite());
        public ICommand SelectPictogramCommand => new Command<IPictogram>(async p => await SelectPictogram(p));

        private readonly IFavoriteService _favoriteService;
        private IRecipe _recipe;

        public RecipeViewModel(IFavoriteService favoriteService)
        {
            if (favoriteService is null)
                throw new ArgumentNullException(nameof(favoriteService));

            _favoriteService = favoriteService;
        }

        public override Task InitializeAsync(IRecipe recipe)
        {
            _recipe = recipe ?? throw new ArgumentNullException(nameof(recipe));

            var messageTask = MessageBroker.Send(this, "View", _recipe);

            Name = _recipe.Name;
            Description = _recipe.Description;
            EstimatedTime = _recipe.EstimatedTime;
            IsFavorite = _recipe.IsFavorite;

            Images = _recipe.Images;

            Ingredients = _recipe.Ingredients;
            Pictograms = _recipe.Pictograms;
            Steps = _recipe.Steps;

            return messageTask;
        }

        private async Task ToggleFavorite()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            IsFavorite = await _favoriteService.ToggleFavorite(_recipe);

            var message = IsFavorite ? "Added to favorites!" : "Removed from favorites!";
            await SnackbarService.SnackbarAsync(message, "Ok", 2750);

            IsBusy = false;
        }

        private Task SelectPictogram(IPictogram pictogram)
        {
            if (pictogram is null)
                return Task.CompletedTask;

            return AlertService.DisplayAlert(pictogram.Name, pictogram.Description, "Close");
        }

        #region Recipe Properties

        public string Name
        {
            get => _name;
            private set => OnPropertyChanged(ref _name, value);
        }
        public string Description
        {
            get => _description;
            private set => OnPropertyChanged(ref _description, value);
        }
        public int EstimatedTime
        {
            get => _estimatedTime;
            set => OnPropertyChanged(ref _estimatedTime, value);
        }
        public bool IsFavorite
        {
            get => _isFavorite;
            set => OnPropertyChanged(ref _isFavorite, value);
        }

        public IEnumerable<string> Images
        {
            get => _images;
            set => OnPropertyChanged(ref _images, value);
        }
        public IEnumerable<IPictogram> Pictograms
        {
            get => _pictograms;
            set => OnPropertyChanged(ref _pictograms, value);
        }
        public IEnumerable<IIngredient> Ingredients
        {
            get => _ingredients;
            set => OnPropertyChanged(ref _ingredients, value);
        }
        public IEnumerable<IStep> Steps
        {
            get => _steps;
            set => OnPropertyChanged(ref _steps, value);
        }

        #endregion

        #region Backing Fields

        private string _name;
        private string _description;
        private int _estimatedTime;
        private bool _isFavorite;

        private IEnumerable<string> _images;
        private IEnumerable<IPictogram> _pictograms;
        private IEnumerable<IIngredient> _ingredients;
        private IEnumerable<IStep> _steps;

        #endregion
    }
}
