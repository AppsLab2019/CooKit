using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Editor.CollectionElementPair;
using CooKit.Models.Editor.Steps;
using CooKit.ViewModels.Editor.Steps;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class EditStepsViewModel : ViewModel
    {
        private Task AddStep()
        {
            throw new NotImplementedException();
        }

        private Task EditStep(IEditorStep step)
        {
            if (step is null)
                return Task.CompletedTask;

            return step switch
            {
                IEditorTextStep _ => EditTextStep(step),
                IEditorImageStep _ => EditImageStep(step),

                _ => throw new NotSupportedException()
            };
        }

        private Task EditTextStep(IEditorStep step)
        {
            var pair = new CollectionElementPair<IEditorStep>(Steps, step);
            return NavigationService.PushAsync<EditTextStepViewModel>(pair);
        }

        private Task EditImageStep(IEditorStep step)
        {
            var pair = new CollectionElementPair<IEditorStep>(Steps, step);
            return NavigationService.PushAsync<EditImageStepViewModel>(pair);
        }

        private void DeleteStep(IEditorStep step)
        {
            if (step is null)
                return;

            Steps.Remove(step);
        }

        public ICommand AddCommand => new Command(async () => await AddStep());
        public ICommand EditCommand => new Command<IEditorStep>(async step => await EditStep(step));
        public ICommand DeleteCommand => new Command<IEditorStep>(DeleteStep);

        public ObservableCollection<IEditorStep> Steps
        {
            get => _steps;
            set => OnPropertyChange(ref _steps, value);
        }

        private ObservableCollection<IEditorStep> _steps;
    }
}
