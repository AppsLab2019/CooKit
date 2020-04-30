using System;
using System.Threading.Tasks;
using CooKit.ViewModels;

namespace CooKit.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task PushAsync(Type viewModel, object parameter = null, bool animated = true);
        Task PushAsync<T>(object parameter = null, bool animated = true) where T : IViewModel;

        Task PopAsync();
        Task PopToRootAsync();

        Task SetRootAsync(Type viewModel, object parameter = null, bool animated = true);
        Task SetRootAsync<T>(object parameter = null, bool animated = true) where T : IViewModel;
    }
}