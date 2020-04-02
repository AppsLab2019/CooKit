﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Autofac;
using CooKit.Models.Steps;
using CooKit.Services;
using CooKit.ViewModels.Editor.Steps;
using CooKit.Views.Editor.Steps;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor
{
    public sealed class StepManagementViewModel : BaseViewModel
    {
        public IStep SelectedStep
        {
            get => _selectedStep;
            set => HandlePropertyChange(ref _selectedStep, value);
        }
        private IStep _selectedStep;

        public ObservableCollection<IStep> Steps { get; }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        private readonly IStepStore _stepStore;
        private readonly Dictionary<string, CreateViewMethod> _options;

        public StepManagementViewModel(ObservableCollection<IStep> steps)
        {
            _selectedStep = null;
            Steps = steps;

            AddCommand = new Command(HandleAdd);
            RemoveCommand = new Command(HandleRemove);

            _stepStore = App.Container.Resolve<IStepStore>();

            _options = new Dictionary<string, CreateViewMethod> {
            {
                "Text", () => new TextStepDesignerView { BindingContext = new TextStepDesignerViewModel(steps) }
            },
            {
                "Image", () => new ImageStepDesignerView { BindingContext = new ImageStepDesignerViewModel(steps) }
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
