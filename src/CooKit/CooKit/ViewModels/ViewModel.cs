using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CooKit.Extensions;
using CooKit.Services.Alerts;
using CooKit.Services.Navigation;
using IContainer = Autofac.IContainer;

namespace CooKit.ViewModels
{
    public class ViewModel : IViewModel, INotifyPropertyChanged
    {
        private readonly Lazy<IAlertService> _lazyAlertService;
        private readonly Lazy<INavigationService> _lazyNavigationService;

        protected IAlertService AlertService => _lazyAlertService.Value;
        protected INavigationService NavigationService => _lazyNavigationService.Value;

        protected ViewModel()
        {
            _lazyAlertService = _container.LazyResolve<IAlertService>();
            _lazyNavigationService = _container.LazyResolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }

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
            set => OnPropertyChange(ref _isBusy, value);
        }

        private bool _isBusy;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange<T>(ref T field, T value, 
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
}
