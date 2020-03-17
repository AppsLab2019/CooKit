using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CooKit.Models.Steps;
using CooKit.Services;
using CooKit.ViewModels.Editor.Steps;
using CooKit.Views.Editor.Steps;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class StepManagementViewModel : BaseViewModel
    {
        public IRecipeStep SelectedStep
        {
            get => _selectedStep;
            set => HandlePropertyChange(ref _selectedStep, value);
        }
        private IRecipeStep _selectedStep;

        public ObservableCollection<IRecipeStep> Steps { get; }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private readonly IRecipeStepStore _stepStore;
        private readonly Dictionary<string, CreateViewMethod> _options;

        public StepManagementViewModel(ObservableCollection<IRecipeStep> steps)
        {
            _selectedStep = null;
            Steps = steps;

            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);

            _stepStore = App.GetRecipeStepStore();

            _options = new Dictionary<string, CreateViewMethod> {
            {
                "Text Only", () => new TextStepDesignerView { BindingContext = new TextStepDesignerViewModel(steps) }
            },
            {
               "Big Image", () => new BigImageStepDesignerView { BindingContext = new BigImageStepDesignerViewModel(steps) }
            }};
        }

        public async void HandleAdd()
        {
            var selected = await DisplayActionSheet("Select Type", null, null, _options.Keys.ToArray());

            if (selected is null)
                return;

            if (_options.TryGetValue(selected, out var method))
                await PushAsync(method());
        }

        public async void HandleRemove()
        {
            if (SelectedStep is null)
                return;

            await _stepStore.RemoveAsync(SelectedStep.Id);

            Steps.Remove(SelectedStep);
            SelectedStep = null;
        }

        private delegate Page CreateViewMethod();
    }
}
