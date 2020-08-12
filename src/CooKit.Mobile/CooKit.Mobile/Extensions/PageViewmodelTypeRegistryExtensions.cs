using CooKit.Mobile.Registries.PageViewmodel;
using CooKit.Mobile.Viewmodels;
using Xamarin.Forms;

namespace CooKit.Mobile.Extensions
{
    public static class PageViewmodelTypeRegistryExtensions
    {
        public static void Register<TPage, TViewmodel>(this IPageViewmodelTypeRegistry registry) 
            where TPage : Page where TViewmodel : IViewmodel
        {
            registry.Register(typeof(TPage), typeof(TViewmodel));
        }
    }
}
