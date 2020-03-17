using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor.Steps
{
    public sealed class BigImageStepDesignerViewModel : BaseViewModel
    {
        public Guid Id => _builder.Id.Value;

        public ObservableCollection<string> AvailableImageLoaders { get; }
        public string SelectedImageLoader { get; set; }
        public string ImageSource { get; set; }

        public ICommand CreateCommand { get; }

        private readonly IBigImageRecipeStepBuilder _builder;
        private readonly ObservableCollection<IRecipeStep> _steps;

        public BigImageStepDesignerViewModel(ObservableCollection<IRecipeStep> steps)
        {
            _steps = steps;

            _builder = App
                .GetRecipeStepStore()
                .CreateBuilder()
                .ToBigImageBuilder();

            var loaderNames = App
                .GetImageStore()
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
