using System;
using System.Threading.Tasks;

namespace CooKit.Mobile.Services.Navigation
{
    public interface INavigationService
    {
        Task PushAsync(Type viewModelType);
        Task PushAsync(Type viewModelType, object parameter);
        Task PopAsync();
    }
}
