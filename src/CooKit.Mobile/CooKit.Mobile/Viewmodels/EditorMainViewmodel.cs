using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Mobile.Extensions;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Ingredients;
using CooKit.Mobile.Models.Steps;
using CooKit.Mobile.Repositories.Pictograms;
using CooKit.Mobile.Services.Pickers;
using CooKit.Mobile.Services.Publish;
using Xamarin.Forms;
using Image = CooKit.Mobile.Models.Images.Image;

namespace CooKit.Mobile.Viewmodels
{
    public class EditorMainViewmodel : ParameterBaseViewmodel<Recipe>
    {
        // TODO: maybe replace this with IPictogramService (logical separation)
        private readonly IPictogramRepository _pictogramRepository;
        private readonly IPublishService _publishService;
        private readonly IImagePicker _imagePicker;

        public ICommand SelectPreviewImageCommand => new Command(async () => await SelectPreviewImageAsync());
        public ICommand AddImageCommand => new Command(async () => await AddImageAsync());
        public ICommand DeleteImageCommand => new Command<Image>(DeleteImage);

        public ICommand NewIngredientCommand => new Command(async () => await NewIngredientAsync());
        public ICommand DeleteIngredientCommand => new Command<Ingredient>(DeleteIngredient);

        public ICommand PublishCommand => new Command(async () => await PublishAsync());

        public EditorMainViewmodel(IPictogramRepository pictogramRepository, IPublishService publishService, 
            IImagePicker imagePicker)
        {
            _pictogramRepository = pictogramRepository;
            _publishService = publishService;
            _imagePicker = imagePicker;

            _images = new ObservableCollection<Image>();
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

        public Image PreviewImage
        {
            get => _previewImage;
            set => OnPropertyChanged(ref _previewImage, value);
        }

        public ObservableCollection<Image> Images
        {
            get => _images;
            set => OnPropertyChanged(ref _images, value);
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

        private Image _previewImage;
        private ObservableCollection<Image> _images;

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

            PreviewImage = recipe.PreviewImage;
            Images = recipe.Images.ToObservableCollection();

            Pictograms = recipe.Pictograms.ToObservableCollection();
            Ingredients = recipe.Ingredients.ToObservableCollection();
            Steps = recipe.Steps.ToObservableCollection();
        }

        private async Task SelectPreviewImageAsync()
        {
            var image = await _imagePicker.PickImageAsync();

            if (image != null)
                PreviewImage = image;
        }

        private async Task AddImageAsync()
        {
            var image = await _imagePicker.PickImageAsync();

            if (image != null)
                Images.Add(image);
        }

        private void DeleteImage(Image image)
        {
            if (image != null)
                Images.Remove(image);
        }

        private async Task NewIngredientAsync()
        {
            var input = await AlertService.InputAsync("New Ingredient", "Enter ingredient text:", "Ok", "Cancel");

            if (string.IsNullOrEmpty(input))
                return;

            var ingredient = new Ingredient(input);
            Ingredients.Add(ingredient);
        }

        private void DeleteIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
                Ingredients.Remove(ingredient);
        }

        private async Task NewStepAsync()
        {

        }

        private async Task PublishAsync()
        {
            await using var loadingAlert = await LoadingAlertService.LoadingAlertAsync("Starting publishing!");
            var progress = new Progress<string>(message => loadingAlert.SetMessage(message));
            var recipe = CreateRecipe();

            try
            {
                await _publishService.PublishRecipeAsync(recipe, progress);
                await AlertService.AlertAsync("Success", "Recipe was successfully published!", "Close");
            }
            catch (Exception e)
            {
                // TODO: format this message based on thrown exception
                await AlertService.AlertAsync("Failure", $"There was an error publishing your recipe: {e.Message}", "Close");
            }
        }

        private Recipe CreateRecipe()
        {
            return new Recipe
            {
                // Id = Id,
                Name = Name,
                Description = Description,
                // EstimatedTime = EstimatedTime,

                PreviewImage = PreviewImage,
                Images = Images,

                Pictograms = Pictograms.ToList(),
                Ingredients = Ingredients.ToList(),
                Steps = Steps.ToList()
            };
        }
    }
}
