using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Editor;
using CooKit.Mobile.Models.Ingredients;
using CooKit.Mobile.Models.Steps;
using CooKit.Mobile.Repositories.Pictograms;
using CooKit.Mobile.Services.Publish;
using Xamarin.Forms;
using XF.Material.Forms.Models;

namespace CooKit.Mobile.Viewmodels.Editor
{
    public class EditorMainViewmodel : ParameterBaseViewmodel<Recipe>
    {
        // TODO: maybe replace this with IPictogramService (logical separation)
        private readonly IPictogramRepository _pictogramRepository;
        private readonly IPublishService _publishService;

        public ICommand PublishCommand => new Command(async () => await PublishAsync());
        public ICommand NewIngredientCommand => new Command(async () => await NewIngredientAsync());

        // TODO: use wrapper class (don't rely on MaterialMenuResult)
        public ICommand InteractIngredientCommand => new Command<MaterialMenuResult>(async result => await InteractIngredientAsync(result));

        public EditorMainViewmodel(IPictogramRepository pictogramRepository, IPublishService publishService)
        {
            _pictogramRepository = pictogramRepository;
            _publishService = publishService;

            _pictograms = new ObservableCollection<Pictogram>();
            _ingredients = new ObservableCollection<Ingredient>();
            _steps = new ObservableCollection<Step>();
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

        public IList<Pictogram> AvailablePictograms
        {
            get => _availablePictograms;
            set => OnPropertyChanged(ref _availablePictograms, value);
        }

        public ObservableCollection<Pictogram> Pictograms
        {
            get => _pictograms;
            set => OnPropertyChanged(ref _pictograms, value);
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get => _ingredients;
            set => OnPropertyChanged(ref _ingredients, value);
        }

        public ObservableCollection<Step> Steps
        {
            get => _steps;
            set => OnPropertyChanged(ref _steps, value);
        }

        private string _name;
        private string _description;

        private IList<Pictogram> _availablePictograms;
        private ObservableCollection<Pictogram> _pictograms;
        private ObservableCollection<Ingredient> _ingredients;
        private ObservableCollection<Step> _steps;

        protected override async Task InitializeAsync(Recipe recipe)
        {
            AvailablePictograms = await _pictogramRepository.GetAllPictogramsAsync();

            if (recipe == null)
                return;

            Name = recipe.Name;
            Description = recipe.Description;

            // create copies in case changes are discarded
            Pictograms = recipe.Pictograms.ToObservableCollection();
            Ingredients = recipe.Ingredients.ToObservableCollection();
            Steps = recipe.Steps.ToObservableCollection();
        }

        private async Task NewIngredientAsync()
        {
            var input = await AlertService.InputAsync("New Ingredient", "Enter ingredient text:", "Ok", "Cancel");

            if (string.IsNullOrEmpty(input))
                return;

            var ingredient = new Ingredient(input);
            Ingredients.Add(ingredient);
        }

        private async Task InteractIngredientAsync(MaterialMenuResult result)
        {
            if (result == null)
                return;

            if (!(result.Parameter is Ingredient ingredient))
                return;

            switch ((IngredientAction) result.Index)
            {
                case IngredientAction.Edit:
                    await EditIngredientAsync(ingredient);
                    break;
                case IngredientAction.Delete:
                    Ingredients.Remove(ingredient);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task EditIngredientAsync(Ingredient ingredient)
        {
            var text = ingredient.Text;
            var input = await AlertService.InputAsync("Edit Ingredient", "Enter modified ingredient text:", 
                text, text, "Ok", "Cancel");

            if (!string.IsNullOrEmpty(input))
                ingredient.Text = input;
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
