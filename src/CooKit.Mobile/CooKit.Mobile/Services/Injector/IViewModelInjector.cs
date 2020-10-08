using Xamarin.Forms;

namespace CooKit.Mobile.Services.Injector
{
    public interface IViewModelInjector
    {
        void InjectViewModel(BindableObject bindableObject);
    }
}
