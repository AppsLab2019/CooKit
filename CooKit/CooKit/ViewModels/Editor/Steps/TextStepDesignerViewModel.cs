using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Autofac;
using CooKit.Models.Steps;
using CooKit.Services;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor.Steps
{
    public sealed class TextStepDesignerViewModel : BaseViewModel
    {
        public Guid Id => _builder.Id.Value;
        public string Text { get; set; }

        public ICommand CreateCommand { get; }

        private readonly ITextStepBuilder _builder;
        private readonly ObservableCollection<IStep> _steps;

        public TextStepDesignerViewModel(ObservableCollection<IStep> steps)
        {
            _steps = steps;

            _builder = App.Container
                .Resolve<IStepStore>()
                .CreateBuilder()
                .ToTextBuilder();

            CreateCommand = new Command(HandleCreate);
        }

        private async void HandleCreate()
        {
            var step = await _builder
                .Text.Set(Text)
                .BuildAsync();

            _steps.Add(step);
            await PopAsync();
        }
    }
}
