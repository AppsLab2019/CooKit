using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class RecipeDesignerViewModel
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageLoader { get; set; }
        public ObservableCollection<string> AvailableImageLoaders { get; }
        public string ImageSource { get; set; }

        public ObservableCollection<IIngredient> AllIngredients { get; }
        public ObservableCollection<IIngredient> SelectedIngredients { get; }

        public ObservableCollection<IPictogram> AllPictograms { get; }
        public ObservableCollection<IPictogram> SelectedPictograms { get; }

        public ICommand CreateCommand { get; }

        private readonly IRecipeBuilder _builder;

        public RecipeDesignerViewModel()
        {
            _builder = App.GetRecipeStore().CreateRecipeBuilder();
            Id = _builder.Id.Value;

            var imageLoaderNames = App
                .GetImageStore()
                .RegisteredLoaders
                .Select(loader => loader.Name);

            AvailableImageLoaders = new ObservableCollection<string>(imageLoaderNames);

            AllIngredients = new ObservableCollection<IIngredient>(
                App.GetIngredientStore().LoadedIngredients);

            AllPictograms = new ObservableCollection<IPictogram>(
                App.GetPictogramStore().LoadedPictograms);

            SelectedIngredients = new ObservableCollection<IIngredient>();
            SelectedPictograms = new ObservableCollection<IPictogram>();

            CreateCommand = new Command(HandleCreate);
        }

        private void HandleCreate()
        {
            _builder
                .Name.Set(Name)
                .Description.Set(Description)
                .ImageLoader.Set(ImageLoader)
                .ImageSource.Set(ImageSource);

            var ingredients = SelectedIngredients
                .Select(ingredient => ingredient.Id)
                .ToArray();

            var pictograms = SelectedPictograms
                .Select(pictogram => pictogram.Id)
                .ToArray();

            _builder
                .IngredientIds.Set(ingredients)
                .PictogramIds.Set(pictograms);

            App.GetRecipeStore().AddRecipe(_builder);
        }
    }
}
