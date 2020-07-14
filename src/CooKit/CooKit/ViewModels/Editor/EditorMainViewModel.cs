using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Extensions;
using CooKit.Models.Editor.Recipes;
using CooKit.Models.Recipes;
using CooKit.Services.Stores;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : ViewModel<IRecipe>
    {
        private IRecipe _recipe;
        private readonly IStore<IRecipe> _store;

        #region Initialization

        public EditorMainViewModel(IStore<IRecipe> store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        public override Task InitializeAsync(IRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            _recipe = recipe;
            EditorRecipe = _recipe.ToEditorRecipe();

            return Task.CompletedTask;
        }

        #endregion

        private async Task Save()
        {
            using var loading = await AlertService.DisplayLoading("Saving...");

            _recipe.Name = EditorRecipe.Name;
            _recipe.Description = EditorRecipe.Description;
            _recipe.EstimatedTime = EditorRecipe.EstimatedTime;
            _recipe.IsFavorite = EditorRecipe.IsFavorite;

            _recipe.PreviewImage = EditorRecipe.PreviewImage;
            _recipe.Images = EditorRecipe.Images;

            _recipe.Ingredients = EditorRecipe.Ingredients;
            _recipe.Pictograms = EditorRecipe.Pictograms;
            _recipe.Steps = EditorRecipe.Steps;

            await _store.Update(_recipe);
        }

        private async Task Exit()
        {
            var shouldDiscard = await AlertService.DisplayAlert("Confirmation",
                "Are you sure you want to exit this editor? You will lose unsaved changes.", "Yes", "No");

            if (!shouldDiscard)
                return;

            await NavigationService.PopAsync();
        }

        private Task ChangeMainInfo()
        {
            return NavigationService.PushAsync<EditMainInfoViewModel>(EditorRecipe);
        }

        private Task ChangeImages()
        {
            return NavigationService.PushAsync<EditImagesViewModel>(EditorRecipe.ObservableImages);
        }

        private async Task ChangeEstimatedTime()
        {
            var rawTime = await AlertService.DisplayInput("Estimated Time",
                "Change estimated time to (min):", EditorRecipe.EstimatedTime.ToString(),
                "e.g.: 30", "Confirm", "Cancel");

            if (string.IsNullOrEmpty(rawTime))
                return;

            var isValid = int.TryParse(rawTime, out var time);

            if (!isValid)
                await ErrorMessage($"\"{rawTime}\" is not a valid time!");
            else if (time < 0)
                await ErrorMessage("Time cannot be negative!");
            else
                EditorRecipe.EstimatedTime = time;
        }

        private Task ChangePreviewImage()
        {
            return NavigationService.PushAsync<EditPreviewImageViewModel>(EditorRecipe);
        }

        private Task ChangePictograms()
        {
            return NavigationService.PushAsync<EditSelectedPictogramsViewModel>(EditorRecipe);
        }

        private Task ChangeIngredients()
        {
            return NavigationService.PushAsync<EditIngredientsViewModel>(EditorRecipe);
        }

        private Task ChangeSteps()
        {
            return NavigationService.PushAsync<EditStepsViewModel>(EditorRecipe.ObservableSteps);
        }

        private Task ErrorMessage(string message)
        {
            return AlertService.DisplayAlert("Error", message, "Ok");
        }

        #region Commands

        public ICommand SaveCommand => new Command(async () => await Save());
        public ICommand ExitCommand => new Command(async () => await Exit());

        public ICommand ChangeMainInfoCommand => new Command(async () => await ChangeMainInfo());
        public ICommand ChangeImagesCommand => new Command(async () => await ChangeImages());
        public ICommand ChangeEstimatedTimeCommand => new Command(async () => await ChangeEstimatedTime());
        public ICommand ChangePreviewImageCommand => new Command(async () => await ChangePreviewImage());
        public ICommand ChangePictogramsCommand => new Command(async () => await ChangePictograms());
        public ICommand ChangeIngredientsCommand => new Command(async () => await ChangeIngredients());
        public ICommand ChangeStepsCommand => new Command(async () => await ChangeSteps());

        #endregion

        public IEditorRecipe EditorRecipe
        {
            get => _editorRecipe;
            set => OnPropertyChanged(ref _editorRecipe, value);
        }

        private IEditorRecipe _editorRecipe;
    }
}
