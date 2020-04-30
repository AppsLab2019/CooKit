using System.Threading.Tasks;
using Autofac;
using CooKit.ViewModels;

namespace CooKit.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync(IContainer container);

        Task PushAsync<T>() where T : IViewModel;
        Task PushAsync<T>(object parameter) where T : IViewModel;
        Task PopAsync();
    }
}