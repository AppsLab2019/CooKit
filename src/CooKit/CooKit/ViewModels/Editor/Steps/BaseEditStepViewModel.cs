using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CooKit.Models.Editor.CollectionElementPair;
using CooKit.Models.Editor.Steps;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor.Steps
{
    public abstract class BaseEditStepViewModel<T> : ViewModel where T : class, IEditorStep
    {
        private ObservableCollection<IEditorStep> _stepCollection;
        private T _originalStep;

        public override Task InitializeAsync(object parameter)
        {
            if (!(parameter is ICollectionElementPair<IEditorStep> pair))
                throw new ArgumentException(nameof(parameter));

            switch (pair.Element)
            {
                case T step:
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

        protected abstract T CreateStep();
        protected abstract T CloneStep(T step);
        protected abstract void ProjectStep(T from, T target);

        protected virtual Task Save()
        {
            if (IsNew)
                _stepCollection.Add(Step);
            else
                ProjectStep(Step, _originalStep);

            return NavigationService.PopAsync();
        }

        public ICommand SaveCommand => new Command(async () => await Save());

        public T Step
        {
            get => _step;
            set => OnPropertyChange(ref _step, value);
        }

        public bool IsNew
        {
            get => _isNew;
            set => OnPropertyChange(ref _isNew, value);
        }

        private T _step;
        private bool _isNew;
    }
}
