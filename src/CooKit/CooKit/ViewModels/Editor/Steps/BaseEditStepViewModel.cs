using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Editor.CollectionElementPair;
using CooKit.Models.Steps;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor.Steps
{
    public abstract class BaseEditStepViewModel<TStep> : ViewModel where TStep : class, IStep
    {
        private ObservableCollection<IStep> _stepCollection;
        private TStep _originalStep;

        public override Task InitializeAsync(object parameter)
        {
            if (!(parameter is ICollectionElementPair<IStep> pair))
                throw new ArgumentException(nameof(parameter));

            switch (pair.Element)
            {
                case TStep step:
                    IsNew = false;
                    Step = CloneStep(step);
                    _originalStep = step;
                    break;

                case null:
                    IsNew = true;
                    Step = CreateStep();
                    _originalStep = null;
                    break;

                default:
                    throw new ArgumentException();
            }

            _stepCollection = pair.Collection;

            return Task.CompletedTask;
        }

        protected abstract TStep CreateStep();
        protected abstract TStep CloneStep(TStep step);
        protected abstract void ProjectStep(TStep from, TStep target);

        protected virtual Task Save()
        {
            if (IsNew)
                _stepCollection.Add(Step);
            else
                ProjectStep(Step, _originalStep);

            return NavigationService.PopAsync();
        }

        public ICommand SaveCommand => new Command(async () => await Save());

        public TStep Step
        {
            get => _step;
            set => OnPropertyChange(ref _step, value);
        }

        public bool IsNew
        {
            get => _isNew;
            set => OnPropertyChange(ref _isNew, value);
        }

        private TStep _step;
        private bool _isNew;
    }
}
