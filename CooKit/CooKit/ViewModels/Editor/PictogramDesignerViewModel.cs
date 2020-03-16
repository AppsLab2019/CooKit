using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CooKit.Models;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class PictogramDesignerViewModel : BaseViewModel
    {
        public Guid Id => _builder.Id.Value;
        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageLoader { get; set; }
        public ObservableCollection<string> AvailableImageLoaders { get; }
        public string ImageSource { get; set; }

        public ICommand CreateCommand { get; }
        private readonly IPictogramBuilder _builder;

        public PictogramDesignerViewModel()
        {
            _builder = App
                .GetPictogramStore()
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
                .Description.Set(Description)
                .ImageLoader.Set(ImageLoader)
                .ImageSource.Set(ImageSource);

            await App.GetPictogramStore().AddAsync(_builder);
            await PopAsync();
        }
    }
}
