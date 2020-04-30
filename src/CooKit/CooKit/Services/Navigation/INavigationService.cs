using System;
using System.Threading.Tasks;
using CooKit.ViewModels;

namespace CooKit.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task PushAsync(Type viewModel);
        Task PushAsync(Type viewModel, object parameter);

        Task PushAsync<T>() where T : IViewModel;
        Task PushAsync<T>(object parameter) where T : IViewModel;

        Task BackAsync();
    }
}