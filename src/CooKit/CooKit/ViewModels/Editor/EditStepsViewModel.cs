using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Editor.CollectionElementPair;
using CooKit.Models.Editor.Steps;
using CooKit.Models.Steps;
using CooKit.ViewModels.Editor.Steps;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditStepsViewModel : ViewModel
    {
        private readonly IList<StepTypeInfo> _types;

        public EditStepsViewModel()
        {
            _types = new List<StepTypeInfo>
            {
                new StepTypeInfo(StepType.Text, typeof(IEditorTextStep), typeof(EditTextStepViewModel)),
                new StepTypeInfo(StepType.Image, typeof(IEditorImageStep), typeof(EditImageStepViewModel))
            };
        }

        public override Task InitializeAsync(object parameter)
        {
            if (!(parameter is ObservableCollection<IEditorStep> steps))
                throw new ArgumentException(nameof(parameter));

            Steps = steps;
            return Task.CompletedTask;
        }

        private async Task AddStep()
        {
            var options = _types
                .Select(type => type.Type.ToString())
                .ToArray();

            var response = await AlertService.DisplaySelectAction("Please select a type:", options);
            const int noResponse = -1;

            if (response == noResponse)
                return;

            var responseType = options[response];
            var info = _types.First(type => type.Type.ToString() == responseType);

            var pair = new CollectionElementPair<IEditorStep>(Steps, null);
            await NavigationService.PushAsync(info.EditViewModelType, pair);
        }

        private Task EditStep(IEditorStep step)
        {
            if (step is null)
                return Task.CompletedTask;

            var stepType = step.GetType();
            var info = _types.FirstOrDefault(type => type.ClassType.IsAssignableFrom(stepType));

            if (info is null)
                throw new NotSupportedException();
            
            var pair = new CollectionElementPair<IEditorStep>(Steps, step);
            return NavigationService.PushAsync(info.EditViewModelType, pair);
        }

        private async Task DeleteStep(IEditorStep step)
        {
            if (step is null)
                return;

            var shouldDelete = await AlertService.DisplayAlert("Confirmation",
                "Are you sure?", "Yes", "No");

            if (shouldDelete)
                Steps.Remove(step);
        }

        public ICommand AddCommand => new Command(async () => await AddStep());
        public ICommand EditCommand => new Command<IEditorStep>(async step => await EditStep(step));
        public ICommand DeleteCommand => new Command<IEditorStep>(async step => await DeleteStep(step));

        public ObservableCollection<IEditorStep> Steps
        {
            get => _steps;
            set => OnPropertyChange(ref _steps, value);
        }

        private ObservableCollection<IEditorStep> _steps;

        internal sealed class StepTypeInfo
        {
            internal StepType Type { get; }
            internal Type ClassType { get; }
            internal Type EditViewModelType { get; }

            public StepTypeInfo(StepType type, Type classType, Type editViewModelType)
            {
                Type = type;
                ClassType = classType;
                EditViewModelType = editViewModelType;
            }
        }
    }
}
