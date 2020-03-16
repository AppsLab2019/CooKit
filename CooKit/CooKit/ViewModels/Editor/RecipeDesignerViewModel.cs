using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CooKit.Models;
using CooKit.Models.Steps;
using CooKit.Views.Editor;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class RecipeDesignerViewModel : BaseViewModel
    {
        public Guid Id => _builder.Id.Value;
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan RequiredTime { get; set; }

        public string ImageLoader { get; set; }
        public ObservableCollection<string> AvailableImageLoaders { get; }
        public string ImageSource { get; set; }

        public ObservableCollection<IIngredient> AllIngredients { get; }
        public ObservableCollection<object> SelectedIngredients { get; }

        public ObservableCollection<IPictogram> AllPictograms { get; }
        public ObservableCollection<object> SelectedPictograms { get; }

        public ObservableCollection<IRecipeStep> Steps { get; }

        public ICommand CreateCommand { get; }
        public ICommand EditStepsCommand { get; }

        private readonly IRecipeBuilder _builder;

        public RecipeDesignerViewModel()
        {
            _builder = App
                .GetRecipeStore()
                .CreateBuilder();

            var imageLoaderNames = App
                .GetImageStore()
                .RegisteredLoaders
                .Select(loader => loader.Name);

            AvailableImageLoaders = new ObservableCollection<string>(imageLoaderNames);

            AllIngredients = new ObservableCollection<IIngredient>(
                App.GetIngredientStore().LoadedObjects);

            AllPictograms = new ObservableCollection<IPictogram>(
                App.GetPictogramStore().LoadedObjects);

            SelectedIngredients = new ObservableCollection<object>();
            SelectedPictograms = new ObservableCollection<object>();

            Steps = new ObservableCollection<IRecipeStep>();

            CreateCommand = new Command(HandleCreate);
            EditStepsCommand = new Command(HandleEditSteps);
        }

        private async void HandleCreate()
        {
            _builder
                .Name.Set(Name)
                .Description.Set(Description)
                .RequiredTime.Set(RequiredTime)
                .ImageLoader.Set(ImageLoader)
                .ImageSource.Set(ImageSource);

            var ingredients = SelectedIngredients
                .Cast<IIngredient>()
                .Select(ingredient => ingredient.Id)
                .ToArray();

            var pictograms = SelectedPictograms
                .Cast<IPictogram>()
                .Select(pictogram => pictogram.Id)
                .ToArray();

            var steps = Steps
                .Select(step => step.Id)
                .ToArray();

            _builder
                .IngredientIds.Set(ingredients)
                .PictogramIds.Set(pictograms)
                .StepIds.Set(steps);

            await App.GetRecipeStore().AddAsync(_builder);
            await PopAsync();
        }

        private async void HandleEditSteps() =>
            await PushAsync(new StepManagementView{ BindingContext = new StepManagementViewModel(Steps) });
    }
}
