using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class IngredientDesignerViewModel
    {
        public Guid Id { get; }
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

            Id = _builder.Id.Value;

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
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
