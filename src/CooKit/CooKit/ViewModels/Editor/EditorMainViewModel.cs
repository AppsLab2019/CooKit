using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Services.Alerts;
using CooKit.Services.Editor;
using CooKit.Services.Stores;
using CooKit.Services.Stores.Ingredients;
using CooKit.Services.Stores.Pictograms;
using CooKit.Services.Stores.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IEditorService _editorService;

        // TODO: inject these dependencies
        private readonly IIngredientStore _ingredientStore;
        private readonly IPictogramStore _pictogramStore;
        private readonly IRecipeStore _recipeStore;

        private IRecipe _recipe;

        public EditorMainViewModel(IAlertService alertService, IEditorService editorService,
            IIngredientStore ingredientStore, IPictogramStore pictogramStore, IRecipeStore recipeStore)
        {
            if (alertService is null)
                throw new ArgumentNullException(nameof(alertService));

            if (editorService is null)
                throw new ArgumentNullException(nameof(editorService));

            if (ingredientStore is null)
                throw new ArgumentNullException(nameof(ingredientStore));

            if (pictogramStore is null)
                throw new ArgumentNullException(nameof(pictogramStore));

            if (recipeStore is null)
                throw new ArgumentNullException(nameof(recipeStore));

            _alertService = alertService;
            _editorService = editorService;

            _ingredientStore = ingredientStore;
            _pictogramStore = pictogramStore;
            _recipeStore = recipeStore;

            InitCommand = new Command(HandleInit);
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

        private async void HandleInit()
        {
            _recipe = _editorService.GetWorkingRecipe();

            if (_recipe is null)
                throw new Exception();

            _name = _recipe.Name;
            _description = _recipe.Description;
            _estimatedTime = _recipe.EstimatedTime;

            //Images = new ObservableCollection<string>(_recipe.Images);
            var pictogramTask = IdsToCollection(_pictogramStore, _recipe.PictogramIds);
            var ingredientTask = IdsToCollection(_ingredientStore, _recipe.IngredientIds);

            await Task.WhenAll(pictogramTask, ingredientTask);

            Pictograms = pictogramTask.Result;
            Ingredients = ingredientTask.Result;

            RaiseAllPropertiesChanged();
        }

        private static async Task<ObservableCollection<T>> IdsToCollection<T>(
            IEntityStore<T> store, IEnumerable<Guid> ids) where T : IEntity
        {
            return new ObservableCollection<T>(await store.GetByIds(ids));
        }

        private async void HandleSave()
        {
            using var loadingDisposable = await _alertService.DisplayLoading("Saving...");

            await _recipeStore.Update(_recipe);
            await CleanUpAndExit();
        }

        private async void HandleDiscard()
        {
            var response = await _alertService.DisplayAlert("Confirmation",
                "Are you sure you want to discard these changes?", "Yes", "No");

            if (!response)
                return;

            await CleanUpAndExit();
        }

        private Task CleanUpAndExit()
        {
            _editorService.ClearWorkingRecipe();
            return Shell.Current.Navigation.PopAsync();
        }

        #region General

        private async void HandleChangeName()
        {
            var name = await _alertService.DisplayInput("Name", "Change name to:", 
                Name, null, "Ok", "Cancel");

            if (string.IsNullOrEmpty(name))
                return;

            Name = name;
        }

        private async void HandleChangeDescription()
        {
            var description = await _alertService.DisplayInput("Description", "Change description to:",
                Description, null, "Ok", "Cancel");

            if (string.IsNullOrEmpty(description))
                return;

            Description = description;
        }

        private async void HandleChangeEstimatedTime()
        {
            var time = await _alertService.DisplayInput("Estimated Time", "Change the time to:",
                EstimatedTime.ToString(), null, "Ok", "Cancel");

            if (string.IsNullOrEmpty(time))
                return;

            if (int.TryParse(time, out var parsedTime))
                EstimatedTime = parsedTime;
            else
                await _alertService.DisplayAlert("Error", $"Invalid input: {time}", "Ok");
        }

        #endregion

        #region Images

        private async void HandleAddImage()
        {
            var source = await _alertService.DisplayInput("Add Image", "Enter the source:",
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

            var confirm = await _alertService.DisplayAlert("Confirmation", 
                "Are you sure you want to delete this image?", "Yes", "No");

            if (!confirm)
                return;

            Images.Remove(source);
        }

        #endregion

        #region Pictograms

        public async void HandleAddPictogram()
        {
            await _alertService.DisplayAlert("Error", "Not implemented!", "Ok");
        }

        public async void HandleSelectPictogram(IPictogram pictogram)
        {
            if (pictogram is null)
                return;

            var response = await _alertService.DisplayAlert(
                pictogram.Name, pictogram.Description, "Delete", "Close");

            if (!response)
                return;

            Pictograms.Remove(pictogram);
        }

        #endregion

        #region Ingredients

        private async void HandleAddIngredient()
        {
            await _alertService.DisplayAlert("Error", "Not implemented!", "Ok");
        }

        private async void HandleDeleteIngredient(IIngredient ingredient)
        {
            if (ingredient is null)
                return;

            var response = await _alertService.DisplayAlert("Confirmation",
                "Are you sure you want to delete this ingredient?", "Yes", "No");

            if (!response)
                return;

            Ingredients.Remove(ingredient);
        }

        #endregion

        #region Commands

        public ICommand InitCommand { get; }
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
