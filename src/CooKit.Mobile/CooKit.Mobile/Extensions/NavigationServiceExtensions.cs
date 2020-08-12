using System.Threading.Tasks;
using CooKit.Mobile.Services.Navigation;
using CooKit.Mobile.Viewmodels;

namespace CooKit.Mobile.Extensions
{
    public static class NavigationServiceExtensions
    {
        public static Task PushAsync<T>(this INavigationService service) where T : IViewmodel
        {
            return service.PushAsync(typeof(T));
        }

        public static Task PushAsync<T>(this INavigationService service, object parameter) where T : IViewmodel
        {
            return service.PushAsync(typeof(T), parameter);
        }

        public static Task PushAsync<T, TParam>(this INavigationService service, TParam parameter)
            where T : ParameterBaseViewmodel<TParam>
        {
            return PushAsync<T>(service, parameter);
        }
    }
}
