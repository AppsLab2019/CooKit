using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CooKit.Extensions;
using CooKit.Services.Alerts;
using CooKit.Services.Messages;
using CooKit.Services.Navigation;
using CooKit.Services.Snackbar;
using IContainer = Autofac.IContainer;

namespace CooKit.ViewModels
{
    public class BaseViewModel : IViewModel, INotifyPropertyChanged
    {
        private readonly Lazy<IAlertService> _lazyAlertService;
        private readonly Lazy<ISnackbarService> _lazySnackbarService;
        private readonly Lazy<INavigationService> _lazyNavigationService;
        private readonly Lazy<IMessageBroker> _lazyMessageBroker;

        protected IAlertService AlertService => _lazyAlertService.Value;
        protected ISnackbarService SnackbarService => _lazySnackbarService.Value;
        protected INavigationService NavigationService => _lazyNavigationService.Value;
        protected IMessageBroker MessageBroker => _lazyMessageBroker.Value;

        protected BaseViewModel()
        {
            _lazyAlertService = _container.LazyResolve<IAlertService>();
            _lazySnackbarService = _container.LazyResolve<ISnackbarService>();
            _lazyNavigationService = _container.LazyResolve<INavigationService>();
            _lazyMessageBroker = _container.LazyResolve<IMessageBroker>();
        }

        public virtual Task InitializeAsync(object parameter) => Task.CompletedTask;

        #region Static Container Initialization

        private static IContainer _container;

        public static void Initialize(IContainer container)
        {
            if (container is null)
                throw new ArgumentNullException(nameof(container));

            _container = container;
        }

        #endregion

        #region IsBusy

        public bool IsBusy
        {
            get => _isBusy;
            set => OnPropertyChanged(ref _isBusy, value);
        }

        private bool _isBusy;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<T>(ref T field, T value, 
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return;

            field = value;
            RaisePropertyChanged(propertyName);
        }

        protected void RaiseAllPropertiesChanged() =>
            RaisePropertyChanged(null);

        protected void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }

    public class ViewModel : BaseViewModel
    {
        public virtual Task InitializeAsync() => Task.CompletedTask;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed override Task InitializeAsync(object parameter)
        {
            return InitializeAsync();
        }
    }

    public abstract class ViewModel<TParamType> : BaseViewModel
    {
        public abstract Task InitializeAsync(TParamType parameter);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed override Task InitializeAsync(object parameter)
        {
            if (parameter is null)
                return InitializeAsync(default);

            if (!(parameter is TParamType cast))
                throw new ArgumentException();

            return InitializeAsync(cast);
        }
    }
}
