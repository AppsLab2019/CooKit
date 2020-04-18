using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Services.Alerts;
using CooKit.Services.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IEditorService _editorService;

        private IRecipeTemplate _template;

        public EditorMainViewModel(IAlertService alertService, IEditorService editorService)
        {
            if (alertService is null)
                throw new ArgumentNullException(nameof(alertService));

            if (editorService is null)
                throw new ArgumentNullException(nameof(editorService));

            _alertService = alertService;
            _editorService = editorService;

            InitCommand = new Command(HandleInit);
            SaveCommand = new Command(HandleSave);
            DiscardCommand = new Command(HandleDiscard);

            ChangeNameCommand = new Command(HandleChangeName);
            ChangeDescriptionCommand = new Command(HandleChangeDescription);
            ChangeEstimatedTimeCommand = new Command(HandleChangeEstimatedTime);

            AddImageCommand = new Command(HandleAddImage);
            DeleteImageCommand = new Command<string>(HandleDeleteImage);

            AddPictogramCommand = new Command(HandleAddPictogram);
            SelectPictogramCommand = new Command<Pictogram>(HandleSelectPictogram);

            AddIngredientCommand = new Command(HandleAddIngredient);
            DeleteIngredientCommand = new Command<Ingredient>(HandleDeleteIngredient);
        }

        private void HandleInit()
        {
            _template = _editorService.GetTemplate();

            if (_template is null)
                throw new Exception();

            // TODO: move this to auto mapper
            _name = _template.Name;
            _description = _template.Description;
            _estimatedTime = _template.EstimatedTime;

            Images = _template.Images;
            Pictograms = _template.Pictograms;
            Ingredients = _template.Ingredients;

            RaiseAllPropertiesChanged();
        }

        private void HandleSave()
        {
            _editorService.ClearTemplate();
        }

        private async void HandleDiscard()
        {
            var response = await _alertService.DisplayAlert("Confirmation",
                "Are you sure you want to discard these changes?", "Yes", "No");

            if (!response)
                return;

            _editorService.ClearTemplate();
            await Shell.Current.GoToAsync("editorMenu");
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

        public async void HandleSelectPictogram(Pictogram pictogram)
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

        private async void HandleDeleteIngredient(Ingredient ingredient)
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

        public ObservableCollection<Pictogram> Pictograms { get; private set; }

        public ObservableCollection<Ingredient> Ingredients { get; private set; }

        #endregion

        #region Backing Fields

        private string _name;
        private string _description;
        private int _estimatedTime;

        #endregion
    }
}
