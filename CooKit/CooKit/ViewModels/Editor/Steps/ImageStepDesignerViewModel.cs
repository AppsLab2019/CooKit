using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Autofac;
using CooKit.Models.Steps;
using CooKit.Services;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor.Steps
{
    public sealed class ImageStepDesignerViewModel : BaseViewModel
    {
        public Guid Id => _builder.Id.Value;

        public ObservableCollection<string> AvailableImageLoaders { get; }
        public string SelectedImageLoader { get; set; }
        public string ImageSource { get; set; }

        public ICommand CreateCommand { get; }

        private readonly IImageStepBuilder _builder;
        private readonly ObservableCollection<IStep> _steps;

        public ImageStepDesignerViewModel(ObservableCollection<IStep> steps)
        {
            _steps = steps;

            var container = App.Container;

            _builder = container.Resolve<IStepStore>()
                .CreateBuilder()
                .ToBigImageBuilder();

            var loaderNames = container.Resolve<IImageStore>()
                .RegisteredLoaders
                .Select(loader => loader.Name);

            AvailableImageLoaders = new ObservableCollection<string>(loaderNames);

            CreateCommand = new Command(HandleCreate);
        }

        public async void HandleCreate()
        {
            var step = await _builder
                .ImageLoader.Set(SelectedImageLoader)
                .ImageSource.Set(ImageSource)
                .BuildAsync();

            _steps.Add(step);
            await PopAsync();
        }
    }
}
