using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Ingredients;
using CooKit.Mobile.Models.Steps;
using Xamarin.Forms;
using Image = CooKit.Mobile.Models.Images.Image;

namespace CooKit.Mobile.Viewmodels.Recipes
{
    public class RecipeDetailViewmodel : ParameterBaseViewmodel<Recipe>
    {
        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => OnPropertyChanged(ref _description, value);
        }

        public TimeSpan EstimatedTime
        {
            get => _estimatedTime;
            set => OnPropertyChanged(ref _estimatedTime, value);
        }

        public IList<Image> Images
        {
            get => _images;
            set => OnPropertyChanged(ref _images, value);
        }

        public IList<Pictogram> Pictograms
        {
            get => _pictograms;
            set => OnPropertyChanged(ref _pictograms, value);
        }

        public IList<Ingredient> Ingredients
        {
            get => _ingredients;
            set => OnPropertyChanged(ref _ingredients, value);
        }

        public IList<Step> Steps
        {
            get => _steps;
            set => OnPropertyChanged(ref _steps, value);
        }

        public ICommand SelectPictogramCommand => new Command<Pictogram>(
            async pictogram => await SelectPictogramAsync(pictogram));

        private string _name;
        private string _description;
        private TimeSpan _estimatedTime;
        private IList<Image> _images;

        private IList<Pictogram> _pictograms;
        private IList<Ingredient> _ingredients;
        private IList<Step> _steps;

        protected override Task InitializeAsync(Recipe recipe)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe));

            Name = recipe.Name;
            Description = recipe.Description;
            EstimatedTime = recipe.EstimatedTime;

            Images = recipe.Images;

            Pictograms = recipe.Pictograms;
            Ingredients = recipe.Ingredients;
            Steps = recipe.Steps;

            return Task.CompletedTask;
        }

        private Task SelectPictogramAsync(Pictogram pictogram)
        {
            return pictogram != null
                ? DisplayPictogramAsync(pictogram)
                : Task.CompletedTask;
        }

        private Task DisplayPictogramAsync(Pictogram pictogram)
        {
            return AlertService.AlertAsync(pictogram.Name, pictogram.Description, "Close");
        }
    }
}
