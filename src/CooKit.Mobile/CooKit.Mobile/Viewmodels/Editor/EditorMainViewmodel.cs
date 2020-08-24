using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Ingredients;
using CooKit.Mobile.Models.Steps;
using CooKit.Mobile.Repositories.Pictograms;
using CooKit.Mobile.Services.Publish;
using Xamarin.Forms;

namespace CooKit.Mobile.Viewmodels.Editor
{
    public class EditorMainViewmodel : ParameterBaseViewmodel<Recipe>
    {
        // TODO: maybe replace this with IPictogramService (logical separation)
        private readonly IPictogramRepository _pictogramRepository;
        private readonly IPublishService _publishService;

        public ICommand PublishCommand => new Command(async () => await PublishAsync());

        public EditorMainViewmodel(IPictogramRepository pictogramRepository, IPublishService publishService)
        {
            _pictogramRepository = pictogramRepository;
            _publishService = publishService;

            _pictograms = new List<Pictogram>();
            _ingredients = new List<Ingredient>();
            _steps = new List<Step>();
        }

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

        public IList<Pictogram> Pictograms
        {
            get => _pictograms;
            set => OnPropertyChanged(ref _pictograms, value);
        }

        public IList<Pictogram> AvailablePictograms
        {
            get => _availablePictograms;
            set => OnPropertyChanged(ref _availablePictograms, value);
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

        private string _name;
        private string _description;

        private IList<Pictogram> _pictograms;
        private IList<Pictogram> _availablePictograms;
        private IList<Ingredient> _ingredients;
        private IList<Step> _steps;

        protected override async Task InitializeAsync(Recipe recipe)
        {
            AvailablePictograms = await _pictogramRepository.GetAllPictogramsAsync();

            // TODO: maybe add default values?
            if (recipe == null)
                return;

            Name = recipe.Name;
            Description = recipe.Description;

            // create copies in case changes are discarded
            Pictograms = recipe.Pictograms.ToList();
            Ingredients = recipe.Ingredients.ToList();
            Steps = recipe.Steps.ToList();
        }

        private async Task PublishAsync()
        {
            await using var loadingAlert = await LoadingAlertService.LoadingAlertAsync("Starting publishing!");
            var progress = new Progress<string>(message => loadingAlert.SetMessage(message));

            // mocking recipe creation for now
            // TODO: replace this with actual logic
            var recipe = new Recipe();

            try
            {
                await _publishService.PublishRecipeAsync(recipe, progress);
                await AlertService.AlertAsync("Success", "Recipe was successfully published!", "Close");
            }
            catch
            {
                // TODO: format this message based on thrown exception
                await AlertService.AlertAsync("Failure", "There was an error publishing your recipe!", "Close");
            }
        }
    }
}
