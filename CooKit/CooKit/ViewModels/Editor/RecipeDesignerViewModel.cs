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
        public TimeSpan RequiredTime { get; set; }

        public string ImageLoader { get; set; }
        public ObservableCollection<string> AvailableImageLoaders { get; }
        public string ImageSource { get; set; }

        public ObservableCollection<IIngredient> AllIngredients { get; }
        public ObservableCollection<object> SelectedIngredients { get; }

        public ObservableCollection<IPictogram> AllPictograms { get; }
        public ObservableCollection<object> SelectedPictograms { get; }

        public ICommand CreateCommand { get; }

        private readonly IRecipeBuilder _builder;

        public RecipeDesignerViewModel()
        {
            _builder = App
                .GetRecipeStore()
                .CreateBuilder();

            Id = _builder.Id.Value;

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

            CreateCommand = new Command(HandleCreate);
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

            _builder
                .IngredientIds.Set(ingredients)
                .PictogramIds.Set(pictograms);

            await App.GetRecipeStore().AddAsync(_builder);
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
