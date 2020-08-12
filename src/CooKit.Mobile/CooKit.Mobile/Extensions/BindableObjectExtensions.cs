using System.Threading.Tasks;
using CooKit.Mobile.Viewmodels;
using Xamarin.Forms;

namespace CooKit.Mobile.Extensions
{
    public static class BindableObjectExtensions
    {
        public static Task InitializeViewmodelAsync(this BindableObject bindableObject, object parameter)
        {
            return bindableObject.BindingContext is IViewmodel viewmodel
                ? viewmodel.InitializeAsync(parameter)
                : Task.CompletedTask;
        }
    }
}
