using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Extensions;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;
using CooKit.Services.Stores.Recipes;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditorMainViewModel : ViewModel
    {
        private IRecipe _recipe;
        private readonly IRecipeStore _store;

        #region Initialization

        public EditorMainViewModel(IRecipeStore store)
        {
            if (store is null)
                throw new ArgumentNullException(nameof(store));

            _store = store;
        }

        public override Task InitializeAsync(object parameter)
        {
            _recipe = parameter as IRecipe;

            if (_recipe is null)
                throw new ArgumentNullException(nameof(parameter));

            _name = _recipe.Name;
            _description = _recipe.Description;
            _estimatedTime = _recipe.EstimatedTime;

            Images = _recipe.Images.ToObservableCollection();

            Pictograms = _recipe.Pictograms.ToObservableCollection();
            Ingredients = _recipe.Ingredients.ToObservableCollection();
            Steps = _recipe.Steps.ToObservableCollection();

            RaiseAllPropertiesChanged();
            return Task.CompletedTask;
        }

        #endregion

        private async Task Save()
        {
            using var loading = await AlertService.DisplayLoading("Saving...");

            _recipe.Name = Name;
            _recipe.Description = Description;
            _recipe.EstimatedTime = EstimatedTime;

            _recipe.Images = Images.ToList();

            _recipe.Ingredients = Ingredients.ToList();
            _recipe.Pictograms = Pictograms.ToList();
            _recipe.Steps = Steps.ToList();

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

        private async Task ChangeEstimatedTime()
        {
            
        }

        #region Commands

        public ICommand SaveCommand => new Command(async () => await Save());
        public ICommand ExitCommand => new Command(async () => await Exit());

        public ICommand ChangeEstimatedTimeCommand => new Command(async () => await ChangeEstimatedTime());

        #endregion

        #region Recipe Properties

        public string Name
        {
            get => _name;
            set => OnPropertyChange(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => OnPropertyChange(ref _description, value);
        }

        public int EstimatedTime
        {
            get => _estimatedTime;
            set => OnPropertyChange(ref _estimatedTime, value);
        }

        public ObservableCollection<string> Images
        {
            get => _images;
            private set => OnPropertyChange(ref _images, value);
        }

        public ObservableCollection<IPictogram> Pictograms
        {
            get => _pictograms;
            private set => OnPropertyChange(ref _pictograms, value);
        }

        public ObservableCollection<IIngredient> Ingredients
        {
            get => _ingredients;
            private set => OnPropertyChange(ref _ingredients, value);
        }

        public ObservableCollection<IStep> Steps
        {
            get => _steps;
            private set => OnPropertyChange(ref _steps, value);
        }

        #endregion

        #region Backing Fields

        private string _name;
        private string _description;
        private int _estimatedTime;

        private ObservableCollection<string> _images;
        private ObservableCollection<IPictogram> _pictograms;
        private ObservableCollection<IIngredient> _ingredients;
        private ObservableCollection<IStep> _steps;

        #endregion
    }
}
