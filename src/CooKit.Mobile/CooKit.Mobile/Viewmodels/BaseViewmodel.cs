﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CooKit.Mobile.Services.Alert;
using CooKit.Mobile.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace CooKit.Mobile.Viewmodels
{
    public abstract class BaseViewmodel : IViewmodel, INotifyPropertyChanged
    {
        protected bool IsBusy { get; set; }
        protected IServiceProvider ServiceProvider { get; private set; }

        protected IAlertService AlertService => _lazyAlertService.Value;
        protected INavigationService NavigationService => _lazyNavigationService.Value;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Lazy<IAlertService> _lazyAlertService;
        private readonly Lazy<INavigationService> _lazyNavigationService;

        protected BaseViewmodel()
        {
            _lazyAlertService = LazyResolveServiceFromProvider<IAlertService>();
            _lazyNavigationService = LazyResolveServiceFromProvider<INavigationService>();
        }

        private Lazy<T> LazyResolveServiceFromProvider<T>()
        {
            return new Lazy<T>(() => ServiceProvider.GetRequiredService<T>());
        }

        public virtual Task InitializeAsync(object parameter) => Task.CompletedTask;
        public virtual Task<bool> OnExitAsync() => Task.FromResult(true);

        protected void OnPropertyChanged<T>(ref T backingField, T newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, newValue))
                return;

            backingField = newValue;
            RaisePropertyChanged(propertyName);
        }

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
