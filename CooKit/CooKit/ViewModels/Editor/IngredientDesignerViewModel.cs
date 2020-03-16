using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class IngredientDesignerViewModel : BaseViewModel
    {
        public Guid Id => _builder.Id.Value;
        public string Name { get; set; }

        public string ImageLoader { get; set; }
        public ObservableCollection<string> AvailableImageLoaders { get; }
        public string ImageSource { get; set; }

        public ICommand CreateCommand { get; }
        private readonly IIngredientBuilder _builder;

        public IngredientDesignerViewModel()
        {
            _builder = App
                .GetIngredientStore()
                .CreateBuilder();

            var imageLoaderNames = App
                .GetImageStore()
                .RegisteredLoaders
                .Select(loader => loader.Name);

            AvailableImageLoaders = new ObservableCollection<string>(imageLoaderNames);

            CreateCommand = new Command(HandleCreate);
        }

        private async void HandleCreate()
        {
            _builder
                .Name.Set(Name)
                .ImageLoader.Set(ImageLoader)
                .ImageSource.Set(ImageSource);

            await App.GetIngredientStore().AddAsync(_builder);
            await PopAsync();
        }
    }
}
