using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Extensions;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Services.Stores.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : ViewModel
    {
        private readonly IRecipeStore _recipeStore;

        private IRecipe _recipe;

        public EditorMainViewModel(IRecipeStore recipeStore)
        {
            if (recipeStore is null)
                throw new ArgumentNullException(nameof(recipeStore));

            _recipeStore = recipeStore;

            SaveCommand = new Command(HandleSave);
            DiscardCommand = new Command(HandleDiscard);

            ChangeNameCommand = new Command(HandleChangeName);
            ChangeDescriptionCommand = new Command(HandleChangeDescription);
            ChangeEstimatedTimeCommand = new Command(HandleChangeEstimatedTime);

            AddImageCommand = new Command(HandleAddImage);
            DeleteImageCommand = new Command<string>(HandleDeleteImage);

            AddPictogramCommand = new Command(HandleAddPictogram);
            SelectPictogramCommand = new Command<IPictogram>(HandleSelectPictogram);

            AddIngredientCommand = new Command(HandleAddIngredient);
            DeleteIngredientCommand = new Command<IIngredient>(HandleDeleteIngredient);
        }

        public override Task InitializeAsync(object parameter)
        {
            _recipe = parameter as IRecipe;

            if (_recipe is null)
                throw new Exception();

            IsBusy = true;

            // TODO: move mapping to a separate function
            _name = _recipe.Name;
            _description = _recipe.Description;
            _estimatedTime = _recipe.EstimatedTime;

            //Images = new ObservableCollection<string>(_recipe.Images);

            Pictograms = _recipe.Pictograms.ToObservableCollection();
            Ingredients = _recipe.Ingredients.ToObservableCollection();

            IsBusy = false;
            RaiseAllPropertiesChanged();
            return Task.CompletedTask;
        }

        private async void HandleSave()
        {
            using var loadingDisposable = await AlertService.DisplayLoading("Saving...");

            await _recipeStore.Update(_recipe);
            await NavigationService.PopAsync();
        }

        private async void HandleDiscard()
        {
            var response = await AlertService.DisplayAlert("Confirmation",
                "Are you sure you want to discard these changes?", "Yes", "No");

            if (!response)
                return;

            await NavigationService.PopAsync();
        }

        #region General

        private async void HandleChangeName()
        {
            var name = await AlertService.DisplayInput("Name", "Change name to:", 
                Name, null, "Ok", "Cancel");

            if (string.IsNullOrEmpty(name))
                return;

            Name = name;
        }

        private async void HandleChangeDescription()
        {
            var description = await AlertService.DisplayInput("Description", "Change description to:",
                Description, null, "Ok", "Cancel");

            if (string.IsNullOrEmpty(description))
                return;

            Description = description;
        }

        private async void HandleChangeEstimatedTime()
        {
            var time = await AlertService.DisplayInput("Estimated Time", "Change the time to:",
                EstimatedTime.ToString(), null, "Ok", "Cancel");

            if (string.IsNullOrEmpty(time))
                return;

            if (int.TryParse(time, out var parsedTime))
                EstimatedTime = parsedTime;
            else
                await AlertService.DisplayAlert("Error", $"Invalid input: {time}", "Ok");
        }

        #endregion

        #region Images

        private async void HandleAddImage()
        {
            var source = await AlertService.DisplayInput("Add Image", "Enter the source:",
                null, null, "Ok", "Cancel");

            if (string.IsNullOrEmpty(source))
                return;

            Images.Add(source);
        }

        private async void HandleDeleteImage(string source)
        {
            if (string.IsNullOrEmpty(source))
                return;

            System.Diagnostics.Debug.WriteLine(source);

            var confirm = await AlertService.DisplayAlert("Confirmation", 
                "Are you sure you want to delete this image?", "Yes", "No");

            if (!confirm)
                return;

            Images.Remove(source);
        }

        #endregion

        #region Pictograms

        public async void HandleAddPictogram()
        {
            await AlertService.DisplayAlert("Error", "Not implemented!", "Ok");
        }

        public async void HandleSelectPictogram(IPictogram pictogram)
        {
            if (pictogram is null)
                return;

            var response = await AlertService.DisplayAlert(
                pictogram.Name, pictogram.Description, "Delete", "Close");

            if (!response)
                return;

            Pictograms.Remove(pictogram);
        }

        #endregion

        #region Ingredients

        private async void HandleAddIngredient()
        {
            await AlertService.DisplayAlert("Error", "Not implemented!", "Ok");
        }

        private async void HandleDeleteIngredient(IIngredient ingredient)
        {
            if (ingredient is null)
                return;

            var response = await AlertService.DisplayAlert("Confirmation",
                "Are you sure you want to delete this ingredient?", "Yes", "No");

            if (!response)
                return;

            Ingredients.Remove(ingredient);
        }

        #endregion

        #region Commands

        public ICommand SaveCommand { get; }
        public ICommand DiscardCommand { get; }

        public ICommand ChangeNameCommand { get; }
        public ICommand ChangeDescriptionCommand { get; }
        public ICommand ChangeEstimatedTimeCommand { get; }

        public ICommand AddImageCommand { get; }
        public ICommand DeleteImageCommand { get; }

        public ICommand AddPictogramCommand { get; }
        public ICommand SelectPictogramCommand { get; }

        public ICommand AddIngredientCommand { get; }
        public ICommand DeleteIngredientCommand { get; }

        #endregion

        #region Recipe Properties

        public string Name
        {
            get => _name;
            private set => OnPropertyChange(ref _name, value);
        }

        public string Description
        {
            get => _description;
            private set => OnPropertyChange(ref _description, value);
        }

        public int EstimatedTime
        {
            get => _estimatedTime;
            private set => OnPropertyChange(ref _estimatedTime, value);
        }

        public ObservableCollection<string> Images { get; private set; }

        public ObservableCollection<IPictogram> Pictograms { get; private set; }

        public ObservableCollection<IIngredient> Ingredients { get; private set; }

        #endregion

        #region Backing Fields

        private string _name;
        private string _description;
        private int _estimatedTime;

        #endregion
    }
}
