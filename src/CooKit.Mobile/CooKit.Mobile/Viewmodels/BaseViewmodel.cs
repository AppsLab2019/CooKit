using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CooKit.Mobile.Services.Alert;
using CooKit.Mobile.Services.Alert.Loading;
using CooKit.Mobile.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace CooKit.Mobile.Viewmodels
{
    public abstract class BaseViewmodel : IViewmodel, INotifyPropertyChanged
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        protected IAlertService AlertService => _lazyAlertService.Value;
        protected ILoadingAlertService LoadingAlertService => _lazyLoadingAlertService.Value;
        protected INavigationService NavigationService => _lazyNavigationService.Value;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Lazy<IAlertService> _lazyAlertService;
        private readonly Lazy<ILoadingAlertService> _lazyLoadingAlertService;
        private readonly Lazy<INavigationService> _lazyNavigationService;

        protected BaseViewmodel()
        {
            _lazyAlertService = LazyResolveServiceFromProvider<IAlertService>();
            _lazyLoadingAlertService = LazyResolveServiceFromProvider<ILoadingAlertService>();
            _lazyNavigationService = LazyResolveServiceFromProvider<INavigationService>();
        }

        private Lazy<T> LazyResolveServiceFromProvider<T>()
        {
            return new Lazy<T>(() => ServiceProvider.GetRequiredService<T>());
        }

        public virtual Task InitializeAsync(object parameter) => Task.CompletedTask;

        protected void OnPropertyChanged<T>(ref T backingField, T newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;

            backingField = newValue;
            RaisePropertyChanged(propertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
